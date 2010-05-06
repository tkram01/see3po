/*
$License$
$Rev$
*/
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading;
using Timer = System.Threading.Timer;
using System.Drawing;
using Nexus3Input;
using RobotCommands;
using RobotHost;
using See3PO;
using FloorTile = See3PO.FloorTile;
using FloorPlan = See3PO.FloorPlan;
using System.Drawing.Drawing2D;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace See3PO
{

    public class Host : IRobotParent
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ui">The user interface for this Host</param>
        public Host(UI ui)
        {
            m_UI = ui;                                  // The user interface

            m_RobotHost = new CRobotHost(this);         // The CRobotHost that will handle communication

            m_pixelsperfoot = 2.0;                      // This is the floorplan scale, and we may not need it

            loadSettings();
        }

        //************************************************************************************************
        //       Public Methods
        //************************************************************************************************

        /// <summary>
        /// Creates a new FloorPlan object in m_Status, then uses that to create a new PathFinder object
        /// </summary>
        /// <param name="FloorPlanImage">an image of the floorplan</param>
        /// <param name="ppf">the scale of the image, in pixels per foot</param>
        public void CreateStatus(Image FloorPlanImage, double ppf)
        {
            m_pixelsperfoot = ppf;
            m_status = new Status(FloorPlanImage, m_pixelsperfoot);

            m_pathfinder = new QGPathFinder(m_status.FloorPlan);
        }

        /// <summary>
        /// Creates a new FloorPlan object in m_Status, then uses that to create a new PathFinder object
        /// </summary>
        /// <param name="FloorPlanImage">an image of the floorplan</param>
        /// <param name="ppf">the scale of the image, in pixels per foot</param>
        public void CreateStatus(FloorPlan fp)
        {
            m_status = new Status(fp);

            m_pathfinder = new QGPathFinder(m_status.FloorPlan);
        }

        /// <summary>
        /// Sends each move in the path to the robot
        /// </summary>
        public void Drive()
        {
            if (m_RobotHost.IsConnected)
            {
                Queue<MoveCommand> path = ConvertPath();
                RequestImage();
                while (m_RobotHost.IsConnected && path.Count != 0)
                {
                    MoveCommand nextMove = path.Dequeue();

                    SendMove(ConvertMove(nextMove));

                    m_UI.PostMessage(nextMove.toString());

                    DateTime sent = DateTime.Now;

                    long ticks = 0;

                    if (nextMove.direction == MoveCommand.Direction.Forward)
                    {
                        Point oldPoint = new Point(Status.Position.location.X, Status.Position.location.Y);
                        Point nextPoint = new Point(Status.Path[1].Position.X, Status.Path[1].Position.Y);
                        m_UI.PostMessage(oldPoint.ToString() + " to " + nextPoint.ToString());
                        while (ticks < nextMove.duration)
                        {
                            double ratio = (double)ticks / (double)nextMove.duration;
                            Point currentPoint = new Point((int)(oldPoint.X * (1.0 - ratio) + nextPoint.X * ratio),
                                (int)(oldPoint.Y * (1.0 - ratio) + nextPoint.Y * ratio));

                            Status.Position = new Position(currentPoint, Status.Position.facing);
                            m_status.FloorPlan.getTile(currentPoint.X, currentPoint.Y).SetPath(false);

                            m_UI.updateUI();
                            ticks = (long)DateTime.Now.Subtract(sent).TotalMilliseconds;
                        }
                        Status.Path.RemoveAt(0);
                    }
                    else
                    {
                        while (ticks < nextMove.duration)
                        {
                            ticks = (long)DateTime.Now.Subtract(sent).TotalMilliseconds;
                        }
                        Status.Path[0].SetPath(false);
                        Status.Position = new Position(Status.Path[0].Position, Status.Position.facing);

                    }

                }
                Status.Position = new Position(Status.EndPoint, Status.Position.facing);
                m_UI.updateUI();
            }
        }

        /// <summary>
        /// Handles sending of system messages to the robot
        /// </summary>
        /// <param name="buffer">message to send, in a byte array</param>
        public void HandleSystemMessage(byte[] buffer)
        {
            switch (buffer[1])
            {
                case CLocalBrainMessage.DISCONNECT:
                    PostMessage("Client disconnect message received.");
                    m_RobotHost.Disconnect(false);
                    m_UI.PostConnection("Disconnected");
                    break;

                default:
                    PostMessage("Unknown system message received from robot client." + buffer);
                    break;
            }
        }

        /// <summary>
        /// Handles sending of data messages to the robot
        /// </summary>
        /// <param name="buffer">message to send, in a byte array</param>
        public void HandleDataMessage(byte[] buffer)
        {
        }

        /// <summary>
        /// Converts an iteger to a byte array
        /// </summary>
        /// <param name="source">original int</param>
        /// <returns>two bytes holding that int</returns>
        private byte[] IntToBytes(int source)
        {
            byte[] bytes = new byte[2];

            bytes[1] = (byte)(source & 0xFF); //low

            bytes[0] = (byte)((source >> 8) & 0xFF); //high

            return bytes;
        }

        /// <summary>
        /// Checks to see if the Robot and Host are connected
        /// </summary>
        /// <returns></returns>
        public bool isConnected()
        {
            return m_RobotHost.IsConnected;
        }

        /// <summary>
        /// Handles connection issues upon closing program
        /// </summary>
        public void OnClosing()
        {
            if (m_RobotHost.IsListening)
                m_RobotHost.StopListening();
            else if (m_RobotHost.IsConnected)
                m_RobotHost.Disconnect(true);
        }

        /// <summary>
        /// Posts a message to the UI
        /// </summary>
        /// <param name="message">the message to be sent</param>
        public void PostMessage(string message)
        {
            m_UI.PostMessage(message);
        }

        /// <summary>
        /// Updates and returns the current Position
        /// </summary>
        /// <returns>the position</returns>
        public Position UpdatePosition()
        {
            if (m_Locator != null)                  // If a locator has been established, then use it
                m_status.Position = m_Locator.GetPosition(m_status.Images);

            return m_status.Position;
        }

        /// <summary>
        /// Set's the destination and gets us a path
        /// </summary>
        public void SetDestination(Point dest)
        {
            m_status.EndPoint = dest;                   // Set the endpoint in the Host's status
            if (m_status.Position != null)
                m_status.Path = m_pathfinder.getPath(); // Recalculate the path
        }

        /// <summary>
        /// Changes the connection status between IsListening, IsConnected and Not Connected
        /// </summary>
        public void ToggleConnection()
        {
            if (m_RobotHost.IsListening)
            {
                m_RobotHost.StopListening();
                m_UI.PostConnection("Disconnected");
            }
            else if (m_RobotHost.IsConnected)
            {
                m_RobotHost.Disconnect(true);
                m_UI.PostConnection("Disconnected");
            }
            else
            {
                m_RobotHost.StartListening();
                m_UI.PostConnection("Connected");
            }
        }

        /// <summary>
        /// Updatest the Connection status between the Host PC and the Robot, then posts
        /// a message back to the UI
        /// </summary>
        public void UpdateStatus()
        {
            string connectionStatus = "";

            if (m_RobotHost.IsListening)
            {
                connectionStatus += "Robot Status: Listening";
                m_UI.PostConnection("Listening");
            }
            else if (m_RobotHost.IsConnected)
            {
                connectionStatus += "Robot Status: Connected";
                m_UI.PostConnection("Connected");
                RequestImage();
                m_RobotHost.Send((char)CRemoteBrainMessage.SERVO + "#16 P1500 #17 P1500 #18 P1500 #19 P1500 #20 P 1500\r", true);
            }
            else
            {
                connectionStatus += "Robot Status: Idle";
                m_UI.PostConnection("Idle");

                if (m_driveTimer != null)
                {
                    m_driveTimer.Dispose();
                    m_driveTimer = null;
                }
            }
        }

        //************************************************************************************************
        //       Private Methods
        //************************************************************************************************

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Queue<MoveCommand> ConvertPath()
        {
            Queue<MoveCommand> newPath = new Queue<MoveCommand>();

            if (Status.Path != null && Status.Path.Count > 0)
            {
                Point facing = facingToVector(Status.Position.facing);  // We'll use this to figure out the current facing and use it as a vector

                Point previous = new Point(Status.Path[0].Position.X, Status.Path[0].Position.Y);

                previous.Offset(new Point(-facing.X, -facing.Y));       // Make a fake previous point for later calculations. 

                int forwardDist = 0;                                    // If we have multiple forwards, it will combine them to one move 
                Point current = Status.Position.location;               // Robot's current point
                Point next;
                for (int i = 0; i < Status.Path.Count; i++)             // we go to the second to last point, the last move will take us from 2nd-to-last to the last point. 
                {
                    next = Status.Path[i].Position;                     // Robot's next point, necessary for deciding if we need to turn. 

                    Point lastDisplacement = new Point(current.X - previous.X, current.Y - previous.Y);// what direction are we currently facing?

                    Point nextDisplacement = new Point(next.X - current.X, next.Y - current.Y); // What direction and how far are we going

                    if (lastDisplacement.X == nextDisplacement.X || lastDisplacement.Y == nextDisplacement.Y) // not turning 
                    {
                        forwardDist += Math.Abs(nextDisplacement.X + nextDisplacement.Y); // One of these will be zero, and we're just looking for the magnitude. 

                        if (forwardDist > 0)
                            newPath.Enqueue(new MoveCommand(MoveCommand.Direction.Forward, forwardDist * FORWARD_MS)); // Then enqueue your forward

                        forwardDist = 0;
                    }
                    else                                                // if we are turning
                    {
                        MoveCommand.Direction turnDirection = DirChange(lastDisplacement, nextDisplacement);

                        newPath.Enqueue(new MoveCommand(turnDirection, turnDirection == MoveCommand.Direction.CW ? TURN_CW_MS : TURN_CCW_MS)); // Enqueue your turn first

                        forwardDist += Math.Abs(nextDisplacement.X + nextDisplacement.Y); // One of these will be zero, and we're just looking for the magnitude. 

                        if (forwardDist > 0)
                            newPath.Enqueue(new MoveCommand(MoveCommand.Direction.Forward, forwardDist * FORWARD_MS)); // Then enqueue your forward

                        forwardDist = 0;                                // reset forward 
                    }

                    previous = current;
                    current = next;// Increment the previous point 
                }
            }
            foreach (MoveCommand move in newPath)
            {
                String msg = "\n\r dir: " + move.direction + " duration" + move.duration;
                m_UI.PostMessage(msg);
            }

            return newPath;
        }

        /// <summary>
        /// Figures out the direction change between two points
        /// </summary>
        /// <param name="PC">The change in coordinates between the previous point and the current point</param>
        /// <param name="CN">The change in coordinates between the current point and the next point</param>
        /// <returns>A move command: Clockwise or Counterclockwise</returns>
        private MoveCommand.Direction DirChange(Point PC, Point CN)     // PC = previous to current, CN = current to next)
        {
            Matrix CW = new Matrix(0, 1, -1, 0, 0, 0);                  // rotation matrices

            Point[] CWarray = { PC };                                    // The matrix methods take point arrays

            CW.TransformPoints(CWarray);                                // Perform a clockwise transform on the point

            if (Math.Sign(CWarray[0].X) == Math.Sign(CN.X) && Math.Sign(CWarray[0].Y) == Math.Sign(CN.Y))
                return MoveCommand.Direction.CW;                        // If the turn is CW, then the signs should be the same (magnitude may not)

            return MoveCommand.Direction.CCW;                           // otherwise, we turned CCW
        }

        /// <summary>
        /// Converts an integer from the Position.facing field to a point for use in vectors
        /// </summary>
        /// <param name="facing"></param>
        /// <returns></returns>
        private Point facingToVector(int facing)
        {
            Point currentDir;     // get starting facing : 0 = East, 90 = North, 180 = West, 270 = South
            if (facing > 315 || facing <= 45)       // East
                currentDir = new Point(1, 0);

            else if (facing > 45 && facing <= 135)  // North
                currentDir = new Point(0, -1);

            else if (facing > 135 && facing <= 225) // West
                currentDir = new Point(-1, 0);

            else                                    // South
                currentDir = new Point(0, 1);

            return currentDir;
        }

        /// <summary>
        /// Converts a MoveCommand objec to a corresponding int array of wheel speeds
        /// </summary>
        /// <param name="move">the move to convert</param>
        /// <returns>an int array holding the left[0] and right[1] wheel speeds and the time[2]</returns>
        private int[] ConvertMove(MoveCommand move)
        {
            int[] speeds = new int[3];
            switch (move.direction)
            {
                case MoveCommand.Direction.Forward:
                    speeds[0] = FORWARD_L;
                    speeds[1] = FORWARD_R;
                    speeds[2] = move.duration;
                    break;

                case MoveCommand.Direction.CCW:
                    speeds[0] = -TURN_CCW;
                    speeds[1] = TURN_CCW;
                    speeds[2] = move.duration;
                    break;

                case MoveCommand.Direction.CW:
                    speeds[0] = TURN_CW;
                    speeds[1] = -TURN_CW;
                    speeds[2] = move.duration;
                    break;
            }
            return speeds;
        }

        /// <summary>
        /// Sends one drive command to the robot
        /// </summary>
        /// <param name="speeds">An int[2] array holding the left and right wheel speeds</param>
        /// <param name="duration">seconds to travel before stopping</param>
        private void SendMove(int[] move)
        {
            short leftSpeed = (short)move[0]; // left
            short rightSpeed = (short)move[1]; // right
            ushort duration = (ushort)move[2];

            if (m_RobotHost.IsConnected)
            {

                byte[] leftSpeeds = BitConverter.GetBytes(leftSpeed);//IntToBytes(leftSpeed);  // split the first 16 bits of the int into two 8-bit bytes

                byte[] rightSpeeds = BitConverter.GetBytes(rightSpeed);//IntToBytes(rightSpeed);// now for the right weel

                byte[] durations = BitConverter.GetBytes(duration / DURATION_INC);//IntToBytes(duration);        

                String msg = "\n\r speeds: " + leftSpeed + " " + rightSpeed + " duration" + duration;
                m_UI.PostMessage(msg);

                m_RobotHost.Send(new byte[] { 0x01, 0x10, 0x11, leftSpeeds[0], leftSpeeds[1], rightSpeeds[0], rightSpeeds[1], durations[0], durations[1], 0xEF }, true);
            }
        }

        //************************************************************************************************
        //       Image Transferring
        //*************************************************************************************

        Thread t1;
        int flag = 0;
        string receivedPath = @"my pic.jpg";
        Queue<byte> data = new Queue<byte>();
        public delegate void MyDelegate();
        Bitmap pic;

        public void RequestImage()
        {
            t1 = new Thread(new ThreadStart(ImgStartListening));
            t1.Start();
            m_RobotHost.Send(new byte[] { 0, 4, 0xEF }, true);
        }

        public class StateObject
        {
            // Client socket.
            public Socket workSocket = null;

            public const int BufferSize = 1024;
            // Receive buffer.
            public byte[] buffer = new byte[BufferSize];
        }


        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public void ImgStartListening()
        {
            byte[] bytes = new Byte[1024];
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, 9050);
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(ipEnd);
                listener.Listen(100);
                while (true)
                {
                    allDone.Reset();
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                    allDone.WaitOne();

                }
            }
            catch (Exception ex)
            {
                m_UI.PostMessage("Failed to Recieve Image");
            }

        }

        public void AcceptCallback(IAsyncResult ar)
        {

            allDone.Set();


            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);


            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
            new AsyncCallback(ReadCallback), state);
            flag = 0;
        }

        public void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            int bytesRead = handler.EndReceive(ar);
            if (bytesRead > 0)
            {

                if (flag == 0)
                {
                    for (int i = 0; i < bytesRead; i++)
                    {
                        data.Enqueue(state.buffer[i]);
                    }

                    flag++;
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
                }
                else
                {
                    for (int i = 0; i < bytesRead; i++)
                    {
                        data.Enqueue(state.buffer[i]);
                    }
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
                }

            }
            else
            {
                BinaryWriter writer = new BinaryWriter(File.Open(receivedPath, FileMode.Append));
                byte[] filedata = data.ToArray();
                filedata.Reverse();
                Stream datastream = new MemoryStream(filedata);
                pic = new Bitmap(datastream);
                writer.Write(filedata);
                writer.Close();
                m_UI.PostImage(pic);
            }
        }


        //************************************************************************************************
        //       Private Attributes
        //************************************************************************************************
        private  int FORWARD_L = 179;      // Forward Wheel Speed Default
        private  int FORWARD_R = 150;      // Forward Wheel Speed Default
        private  int FORWARD_MS = 2000;         // forward durations in ms

        private  int TURN_CW = 155;         // Turning Wheel Speed Default
        private  int TURN_CW_MS = 1800;         // Turning durations in ms

        private  int TURN_CCW = 170;         // Turning Wheel Speed Default
        private  int TURN_CCW_MS = 2100;

        private  int DURATION_INC = 50;        // 

        private UI m_UI;                            // The User Interface

        private Status m_status;                    // The main data struture

        private QGPathFinder m_pathfinder;            // The Path Finder

        private Locator m_Locator;                  // The Locator, usually Tyler's Program

        private CRobotHost m_RobotHost;             // The Host Protocol for the robot

        private double m_pixelsperfoot;

        delegate void DGuiCallVoid();               // The Delegate for Void calls 
        delegate void DGuiCallString(string str);   // The Delegate for string calls
        delegate void DGuiCallBuffer(byte[] buffer);// The Delegate for byte calls

        private ThreadStart t_SendPath;             // Thread to Send the path / Driving
        private Thread t_SendPathThread;

        private Timer m_driveTimer;                 // Thread for Driving
        private TimerCallback m_callback;


        //************************************************************************************************
        //       Getters and Setters for private attributes
        //************************************************************************************************

        /// <summary>
        /// The Locator (Tyler's Program)
        /// </summary>
        public Locator Locator
        {
            get { return m_Locator; }
            set { m_Locator = value; }
        }

        /// <summary>
        /// The Main Data Structure, holding Floor Plan, Path and Location
        /// </summary>
        public Status Status
        {
            get { return m_status; }
        }

        /// <summary>
        /// The Robot's current Position
        /// </summary>
        public Position Position
        {
            get
            {
                if (m_Locator != null) // If a locator has been established, then use it
                    m_status.Position = m_Locator.GetPosition(m_status.Images);

                return m_status.Position;
            }
            set
            {
                m_status.Position = new Position(value.location.X, value.location.Y, value.facing);
            }
        }



        // open a defaults file and load the defaults
        public void loadSettings()
        {
            string file = ".\\settings\\settings.txt";
            try
            {
                if (!File.Exists(file))
                {

                    using (StreamWriter sw = File.CreateText(file))
                    {
                        m_UI.PostMessage("No Defaults File Found");
                    }
                }

                using (StreamReader reader = new StreamReader(File.OpenRead(file)))
                {
                    string line = reader.ReadLine();
                    string[] words = line.Split('=');
                    while ((line = reader.ReadLine()) != null)
                    {
                        try{
                            if (words[0] == "FORWARD_L")
                            {
                                FORWARD_L = int.Parse(words[1]);
                            }
                            if (words[0] == "FORWARD_R")
                            {
                                FORWARD_R = int.Parse(words[1]);
                            }
                            if (words[0] == "FORWARD_MS")
                            {
                                FORWARD_MS = int.Parse(words[1]);
                            }
                            if (words[0] == "TURN_CW")
                            {
                                TURN_CW = int.Parse(words[1]);
                            }
                            if (words[0] == "TURN_CW_MS")
                            {
                                TURN_CW_MS = int.Parse(words[1]);
                            }
                            if (words[0] == "TURN_CCW")
                            {
                                TURN_CCW = int.Parse(words[1]);
                            }
                            if (words[0] == "TURN_CCW_MS")
                            {
                                TURN_CCW_MS = int.Parse(words[1]);
                            }
                            if (words[0] == "DURATION_INC")
                            {
                                DURATION_INC = int.Parse(words[1]);
                            }
                        } catch (Exception e){m_UI.PostMessage("error parsing: " + line);}
                    }
                }
            }
            catch (IOException e)
            {
                m_UI.PostMessage("file not found:" + file);
            }
        }
    }
}

