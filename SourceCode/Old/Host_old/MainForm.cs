using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Timer = System.Threading.Timer;
using System.Drawing.Drawing2D;

using Nexus3Input;
using RobotCommands;
using RobotHost;

namespace Host
{
    
	public partial class MainForm : Form, IRobotParent
	{
        public const int FORWARD_SPEED = 5;
        public const int TURN_SPEED = 5;


        public enum fpState { NONE, IMAGE, DRAWSCALE, SETROBOT, SETDEST, HAVEPATH };

        public fpState m_fpState;

		delegate void DGuiCallVoid();
		delegate void DGuiCallString(string str);
		delegate void DGuiCallBuffer(byte[] buffer);
        delegate void DDrawFloor();
        
        DDrawFloor t_DrawFloorDelegate;
        Timer t_DrawScaleTimer;

        ThreadStart t_SendPath;
        Thread t_SendPathThread;


        Status m_status;
        public Status status {
            get { 
                return m_status; 
            }
        }
        PathFinder m_pathfinder;

		CRobotHost m_host;
		CWebcam m_camera;

		Timer m_driveTimer;
        
        TimerCallback m_callback;
        
        Bitmap m_floorPlanImage;

        Graphics m_fg;

        Point m_center;
        Point m_highlight;
        bool m_highlighted;
        bool highlighted 
        {
            set { m_highlighted = value; }    
        }
        
        Image m_robotImage;

        Image m_destImage;
        
        Point m_pixelsperfootStart;
        Point m_pixelsperfootEnd;
        public double m_pixelsperfoot;

        short m_leftSpeed = 0;
        short m_rightSpeed = 0;

        double m_ratioX;
        double m_ratioY;

        public MainForm()
        {
            InitializeComponent();

            m_host = new CRobotHost(this);

            m_destImage = Image.FromFile("Images\\destImage.png");

            m_camera = new CWebcam(livePanel, null, false);
            m_camera.Initialize();
            m_camera.SetReady();

            m_center = new Point(floorPlanPanel.Width / 2, floorPlanPanel.Height / 2);
            
            m_fg = Graphics.FromHwnd(floorPlanPanel.Handle);
            
            m_pixelsperfoot = 2.0;
            
            m_callback = new TimerCallback(DrawScale);
            m_fpState = fpState.NONE;

            t_DrawFloorDelegate = new DDrawFloor(DrawFloor);
            

        }

		protected override void OnClosing(CancelEventArgs e)
		{
			if(m_host.IsListening)
				m_host.StopListening();
			else if(m_host.IsConnected)
				m_host.Disconnect(true);

			base.OnClosing(e);
		}

        public void HandleSystemMessage(byte[] buffer)
        {
            if (InvokeRequired)
            {
                Invoke(new DGuiCallBuffer(HandleSystemMessage), buffer);
                return;
            }

            switch (buffer[1])
            {
                case CLocalBrainMessage.DISCONNECT:
                    PostMessage("Client disconnect message received.");
                    m_host.Disconnect(false);
                    break;

                default:
                    PostMessage("Unknown system message received from robot client.");
                    break;
            }
        }

        public void HandleDataMessage(byte[] buffer)
        {
            if (InvokeRequired)
            {
                Invoke(new DGuiCallBuffer(HandleDataMessage), buffer);
                return;
            }
        }

        public void PostMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new DGuiCallString(PostMessage), message);
                return;
            }

            messageBox.Text = message + "\r\n" + messageBox.Text;
        }

        public void UpdateStatus()
		{
			if(InvokeRequired)
			{
				Invoke(new DGuiCallVoid(UpdateStatus));
				return;
			}

			string connectionStatus = "";

			if(m_host.IsListening)
			{
                connectionStatus += "Robot Status: Listening";
				connectMenuItem.Text = "Stop Listening";
			}
			else if(m_host.IsConnected)
			{
                connectionStatus += "Robot Status: Connected";
                connectMenuItem.Text = "Disconnect";
				m_host.Send((char)CRemoteBrainMessage.SERVO + "#16 P1500 #17 P1500 #18 P1500 #19 P1500 #20 P 1500\r", true);
			}
			else
			{
                connectionStatus += "Robot Status: Idle";
				connectMenuItem.Text = "Listen";

                if (m_driveTimer != null)
				{
                    m_driveTimer.Dispose();
                    m_driveTimer = null;
				}
			}

			statusLabel.Text = connectionStatus;
		}

        private void ConnectMenuItem_Click(object sender, EventArgs e)
        {
            if (m_host.IsListening)
                m_host.StopListening();
            else if (m_host.IsConnected)
                m_host.Disconnect(true);
            else
                m_host.StartListening();
        }

        public void DrawFloor()
        {
            try
            {
                Bitmap buffer = new Bitmap(floorPlanPanel.Width, floorPlanPanel.Height);
                Graphics bg = Graphics.FromImage(buffer);
                SolidBrush overlay = new SolidBrush(Color.FromArgb(0, Color.White));
                String instructions = "";

                switch (m_fpState)
                {
                    case fpState.NONE:
                        instructions = "Please Load or Import a Floor Plan";
                        break;

                    case fpState.IMAGE:
                        instructions = "Click the floor plan to draw a scale";
                        bg.DrawImage(m_floorPlanImage, 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        overlay = new SolidBrush(Color.FromArgb(10, Color.Green));
                        break;

                    case fpState.DRAWSCALE:
                        instructions = "Click again to draw a known measurement";
                        bg.DrawImage(m_floorPlanImage, 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        overlay = new SolidBrush(Color.FromArgb(10, Color.Green));
                        bg.DrawLine(new Pen(Color.Blue), (PointF)m_pixelsperfootStart, floorPlanPanel.PointToClient(System.Windows.Forms.Control.MousePosition));
                        break;

                    case fpState.SETROBOT:
                        bg.DrawImage(m_status.floorPlan.toImage(), 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        instructions = "Click the floor plan to set the robot's current location";
                        overlay = new SolidBrush(Color.FromArgb(10, Color.Red));
                        break;

                    case fpState.SETDEST:
                        instructions = "Click the floor plan to set the destination";
                        bg.DrawImage(m_status.floorPlan.toImage(), 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        break;

                    case fpState.HAVEPATH:
                        instructions = "Click the floor plan to change the destination";
                        bg.DrawImage(m_status.floorPlan.toImage(), 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        if (m_status.path != null)
                            bg.DrawImage(DrawMoves(), 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        else
                            instructions = "unwalkable destination";
                        break;
                }
                bg.FillRectangle(overlay, 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                bg.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit; 
                bg.DrawString(instructions, new Font("Times New Roman", 16), new SolidBrush(Color.White), new Point(4, 6));
                bg.DrawString(instructions, new Font("Times New Roman", 16), new SolidBrush(Color.Blue), new Point(5, 5));
                m_fg.DrawImage(buffer, 0, 0);

            }
            catch (InvalidOperationException e) { PostMessage(e.ToString()); };
        }

        private void DrawScale(object State) 
        {
            if (InvokeRequired)
            {
                Invoke(new DDrawFloor(DrawFloor), null);
                return;
            }
        }

        private void SendPath() 
        {
            if (m_host.IsConnected)
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

        private void Drive(int[] speeds, short duration )
        {
            int leftSpeed = speeds[0]; // left
            int rightSpeed = speeds[1]; // right

            if (m_host.IsConnected)
            {
                byte[] leftSpeeds = intToBytes(leftSpeed);

                byte[] rightSpeeds = intToBytes(rightSpeed);

                byte[] durations = intToBytes(duration);
                
                String msg = "\n\r speeds: " + leftSpeed + " " + rightSpeed;
                PostMessage(msg);

                m_host.Send(new byte[] {0x01, 0x10 , 0x11, leftSpeeds[0], leftSpeeds[1], rightSpeeds[0], rightSpeeds[1], 0xEF, durations[0], durations[1] }, true);
            }
        }

        private void Click_FloorPlan(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
            case MouseButtons.Left:
                switch (m_fpState)
                {
                case fpState.IMAGE:
                    m_pixelsperfootStart = e.Location;
                    m_fpState = fpState.DRAWSCALE;
                    t_DrawScaleTimer = new Timer(m_callback, null, 0, 100);
                    break;

                case fpState.DRAWSCALE:
                    m_pixelsperfootEnd = e.Location;
                    double scaleLength = Length(m_pixelsperfootStart.X - m_pixelsperfootEnd.X, m_pixelsperfootStart.Y - m_pixelsperfootEnd.Y);
                    SetScale(scaleLength);
                    break;

                case fpState.SETROBOT:
                    PlaceRobot(sender, e);
                    break;

                case fpState.SETDEST:
                case fpState.HAVEPATH:
                    if (m_status != null)
                    {
                        SetDestination(sender, e);
                    }
                    break;

                }
                break;

            case MouseButtons.Right:
                floorPlanContext.Show(System.Windows.Forms.Control.MousePosition);
                break;

            case MouseButtons.Middle:
                break;

            default:
                break;
            }
        }

        private void Click_SetScale(object sender, EventArgs e)
        {
            SetScale(10.0);
        }

        private void SetScale(double scaleLength)
        {
            m_fpState = fpState.IMAGE;
            using (ScaleForm sf = new ScaleForm(scaleLength, scaleLength / m_pixelsperfoot, this))
            {
                sf.ShowDialog();
                m_pixelsperfoot = sf.m_scale;
                m_status = new Status(m_floorPlanImage, m_pixelsperfoot);
                m_pathfinder = new QGPathFinder(m_status, this); 
                m_ratioX = (double)(m_status.floorPlan.getXTileNum()) / (double)(floorPlanPanel.Width);
                m_ratioY = (double)m_status.floorPlan.getYTileNum() / (double)floorPlanPanel.Height;
                m_fpState = fpState.SETDEST;
            }
            if (t_DrawScaleTimer != null)
            {
                t_DrawScaleTimer.Dispose();
            }
            
            m_fpState = fpState.SETDEST;
            DrawFloor();
        }

        private void Click_Import(object sender, EventArgs e)
        {
            importImageDialog.InitialDirectory = ".\floorplan";
            importImageDialog.ShowDialog();
            try
            {
                m_floorPlanImage = new Bitmap(Image.FromFile(importImageDialog.FileName));
            }
            catch (Exception) { }
            //m_status = new Status(m_floorPlanImage, 3);
            m_fpState = fpState.IMAGE;
            SetScale(10);
            DrawFloor();
        }
		
        
        private void Load_MainForm(object sender, EventArgs e)
        {

        }

        private void Paint_FloorPlanPanel(object sender, PaintEventArgs e)
        {
            DrawFloor();
        }

        private void SetDestination(object sender, MouseEventArgs e)
        {
            m_status.position = getPosition();

            PostMessage(m_status.endPoint.ToString());

            m_status.endPoint = PanelToFloorPlan(new Point(e.X, e.Y));

            m_status.path = m_pathfinder.getPath();

            m_fpState = fpState.HAVEPATH;

            DrawFloor();
        }

        private void PlaceRobot(object sender, MouseEventArgs e)
        {
            MoveRobot(PanelToFloorPlan(e.Location), m_status.position.facing); //fix the facing
        }

        public void MoveRobot(Point loc, int facing)
        {
            m_status.position = new Position(loc.X, loc.Y, facing);  
            if (m_status.path != null)
                m_fpState = fpState.HAVEPATH;
            else
                m_fpState = fpState.SETDEST;
            DrawFloor();
        }

        private Point CenterPointOnImage(Point original, Image image) 
        {
            return new Point(original.X - (image.Width / 4), original.Y - (image.Height / 4));
        }

        private Point CenterPoint(Point original, int size)
        {
            return new Point(original.X - (size/2), original.Y - (size/2));
        }

        private void Click_PlaceRobot(object sender, EventArgs e)
        {
                m_fpState = fpState.SETROBOT;

                DrawFloor();
        }

        private void Click_SetDestination(object sender, EventArgs e)
        {
            m_fpState = fpState.SETDEST;

            DrawFloor();
        }

        private Image DrawMoves()
        {
            Bitmap trail = new Bitmap(floorPlanPanel.Width, floorPlanPanel.Height);

            Graphics s = Graphics.FromImage(trail);

            Point lineStart = new Point(FloorPlanToPanel(m_status.position.location).X, FloorPlanToPanel(m_status.position.location).Y);
            
            Point lineEnd = new Point(lineStart.X, lineStart.Y);

            foreach (FloorTile tile in m_status.path) {

                lineStart = lineEnd;

                lineEnd = FloorPlanToPanel(tile.Position);

                s.DrawLine(new Pen(new SolidBrush(Color.Blue)), lineStart, lineEnd);
            }

            if (m_highlighted)
                s.DrawEllipse(new Pen(new SolidBrush(Color.BlueViolet), 2), new Rectangle(CenterPoint(m_highlight, 4), new Size(3,3)));

            //Point[] currentDir = new Point[1];// get starting facing : 0 = East, 90 = North, 180 = West, 270 = South
            //if (m_status.position.facing > 315 || m_status.position.facing <= 45)
            //    currentDir[0] = new Point(1, 0);
            //else if (m_status.position.facing > 45 && m_status.position.facing <= 135)
            //    currentDir[0] = new Point(0, -1);
            //else if (m_status.position.facing > 135 && m_status.position.facing <= 225)
            //    currentDir[0] = new Point(-1, 0);
            //else if (m_status.position.facing > 225 && m_status.position.facing <= 315)
            //    currentDir[0] = new Point(0, 1);

            //// Rotation Matrices
            //Matrix CW = new Matrix(0, 1, -1, 0, 0, 0);
            //Matrix CCW = new Matrix(0, -1, 1, 0, 0, 0);

            //foreach (FloorTile cmd in m_status.path)
            //{
            //    switch (cmd.direction)
            //    {
            //    case MoveCommand.Direction.Forward:
            //        Point move = new Point((int)((double)(currentDir[0].X * cmd.distance) / m_ratioX), (int)((double)(currentDir[0].Y * cmd.distance) / m_ratioY));
            //        lineEnd.Offset(move);
            //        break;
            //    case MoveCommand.Direction.CCW:
            //        s.DrawLine(new Pen(new SolidBrush(Color.Blue)), lineStart, lineEnd);
            //        lineStart = new Point(lineEnd.X, lineEnd.Y);
            //        CCW.TransformPoints(currentDir);
            //        break;
            //    case MoveCommand.Direction.CW:
            //        s.DrawLine(new Pen(new SolidBrush(Color.Blue)), lineStart, lineEnd);
            //        lineStart = new Point(lineEnd.X, lineEnd.Y);
            //        CW.TransformPoints(currentDir);
            //        break;
            //    }  
            //}
            return trail;
        }

        private Position getPosition() 
        {
            if (m_status.position == null)
                m_status.position = new Position(PanelToFloorPlan(m_center), 0); // eventually, call Tyler's program

            //m_status.floorPlan.setStartTile(m_status.position.location.X, m_status.position.location.Y);

            return m_status.position;
        }

        private Point PanelToFloorPlan(Point panelPoint)
        {
            return new Point((int)(panelPoint.X * m_ratioX), (int)(panelPoint.Y * m_ratioY));
        }

        private Point FloorPlanToPanel(Point fpPoint) 
        {
            return new Point((int)(fpPoint.X / m_ratioX), (int)(fpPoint.Y / m_ratioY));
        }

        private void goMenuItem_Click(object sender, EventArgs e)
        {
            if (m_fpState == fpState.HAVEPATH)
            {
                t_SendPath = new ThreadStart(SendPath);

                t_SendPathThread = new Thread(t_SendPath);

                t_SendPathThread.Start();
            }
        }

        private byte[] intToBytes(int source)
        {
            byte[] bytes = new byte[2];

            bytes[1] = (byte)(source & 0xFF); //low
            bytes[0] = (byte)((source >> 8) & 0xFF); //high

            return bytes;
        }

        private double Length(double x, double y)
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        private void Click_ShowPath(object sender, EventArgs e)
        {
            PathForm pathform = new PathForm(this);

            pathform.Show();
        }

        
        public void highlightPoint(Point point)
        {
            m_highlighted = true;

            m_highlight = FloorPlanToPanel(point);

            DrawFloor();
        }

        public List<FloorTile> condenseList(List<FloorTile> path)
        {
            List<FloorTile> condensedList = new List<FloorTile>();

            FloorTile lastTile = path[0];


            condensedList.Add(path[0]);

            for (int i = 1; i < path.Count; i++)
            {
                if (path[i].Position.Y == lastTile.Position.Y) // moving vertically
                {
                    while (i < path.Count && path[i].Position.Y == lastTile.Position.Y && i < path.Count - 1) // walk until the next turn
                    {
                        i++;
                    }
                }
                else                                           // moving horizontally
                {
                    while (i < path.Count && path[i].Position.X == lastTile.Position.X) // walk until the next turn
                    {
                        i++;
                    }
                }
                lastTile = path[i - 1];
                condensedList.Add(path[i - 1]); // add the turning point to the new list;
            }

            return condensedList;
        }
	}
}
