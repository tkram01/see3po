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
using FloorPlanAndTile;
using FloorTile = FloorPlanAndTile.FloorTile;
using FloorPlan = FloorPlanAndTile.FloorPlan;

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
            m_UI = ui;
            m_Locator = m_UI.getDefaultLocator();

            m_RobotHost = new CRobotHost(this);

            m_pixelsperfoot = 2.0;
        }

        /// <summary>
        /// Creates a new FloorPlan object in m_Status, then uses that to create a new PathFinder object
        /// </summary>
        /// <param name="FloorPlanImage">an image of the floorplan</param>
        /// <param name="ppf">the scale of the image, in pixels per foot</param>
        public void CreateFloorPlan(Image FloorPlanImage, double ppf)
        {
            m_pixelsperfoot = ppf;

            m_status = new Status(FloorPlanImage, m_pixelsperfoot); // 

            m_pathfinder = new QGPathFinder(m_status, this);
        }

        /// <summary>
        /// The Robot's current position
        /// </summary>
        public Position CurrentPosition 
        {
            get {
                if (m_Locator != null) // If a locator has been established, then use it
                    m_status.Position = m_Locator.GetPosition(m_status.Images);

                return m_status.Position;
            }
            set {
                m_status.Position = new Position(value.location.X, value.location.Y, value.facing);
            }
        }

        /// <summary>
        /// Sends one drive command to the robot
        /// </summary>
        /// <param name="speeds">An int[2] array holding the left and right wheel speeds</param>
        /// <param name="duration">seconds to travel before stopping</param>
        private void Drive(int[] speeds, short duration)
        {
            int leftSpeed = speeds[0]; // left
            int rightSpeed = speeds[1]; // right

            if (m_RobotHost.IsConnected)
            {
                byte[] leftSpeeds = IntToBytes(leftSpeed);

                byte[] rightSpeeds = IntToBytes(rightSpeed);

                byte[] durations = IntToBytes(duration);

                String msg = "\n\r speeds: " + leftSpeed + " " + rightSpeed;
                PostMessage(msg);

                m_RobotHost.Send(new byte[] { 0x01, 0x10, 0x11, leftSpeeds[0], leftSpeeds[1], rightSpeeds[0], rightSpeeds[1], 0xEF, durations[0], durations[1] }, true);
            }
        }

        /// <summary>
        /// Converts a MoveCommand objec to a corresponding int array of wheel speeds
        /// </summary>
        /// <param name="move">the move to convert</param>
        /// <returns>an int array holding the left and right wheel speeds</returns>
        private int[] GetSpeeds(MoveCommand move)
        {
            int[] speeds = new int[2];
            switch (move.direction)
            {
                case MoveCommand.Direction.Forward:
                    speeds[0] = speeds[1] = FORWARD_SPEED;
                    break;

                case MoveCommand.Direction.CCW:
                    speeds[0] = -TURN_SPEED;
                    speeds[1] = TURN_SPEED;
                    break;

                case MoveCommand.Direction.CW:
                    speeds[0] = TURN_SPEED;
                    speeds[1] = -TURN_SPEED;
                    break;
            }
            return speeds;
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
                    break;

                default:
                    PostMessage("Unknown system message received from robot client.");
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
        /// Send a path to the robot
        /// </summary>
        private void SendPath()
        {
            if (m_RobotHost.IsConnected)
            {

                //List<FloorTile> moves = m_status.path;//(List<MoveCommand>)state;
                //foreach(FloorTile move in moves){
                //    int[] speeds = GetSpeeds(move);
                //    Drive(speeds, (short)move.distance);
                //    PostMessage(move.toString());
                //    Thread.Sleep(move.distance * 500);
                //}
            }
        }

        /// <summary>
        /// Changes the connection status between IsListening, IsConnected and Not Connected
        /// </summary>
        private void ToggleConnection()
        {
            if (m_RobotHost.IsListening)
                m_RobotHost.StopListening();
            else if (m_RobotHost.IsConnected)
                m_RobotHost.Disconnect(true);
            else
                m_RobotHost.StartListening();
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
//       Private Attributes
//************************************************************************************************
        private const int FORWARD_SPEED = 5;        // Forward Wheel Speed Default
        private const int TURN_SPEED = 5;           // Turning Wheel Speed Default

        private UI m_UI;                            // The User Interface

        private Status m_status;                    // The main data struture

        private PathFinder m_pathfinder;            // The Path Finder

        private Locator m_Locator;                  // The Locator, usually Tyler's Program

        private CWebcam m_camera;                   // A webcam for live viewing

        private CRobotHost m_RobotHost;                  // The Host Protocol for the robot

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
            get { return Locator; }
            set { Locator = value; }
        }

        /// <summary>
        /// The Main Data Structure, holding Floor Plan, Path and Location
        /// </summary>
        public Status Status
        {
            get { return m_status; }
        }
            
    }
}
