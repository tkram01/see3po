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

//using Nexus3Input;
using RobotCommands;
using RobotHost;

namespace See3PO
{
    
	public partial class MainForm : Form, IRobotParent
	{
        enum fpState { NONE, IMAGE, SCALE, FLOORPLAN, DESTINATION };
        fpState m_fpState;

		delegate void DGuiCallVoid();
		delegate void DGuiCallString(string str);
		delegate void DGuiCallBuffer(byte[] buffer);

        Status m_status;

		CRobotHost m_host;
		//CWebcam m_camera;

		Timer m_driveTimer;
        System.Threading.Timer m_drawTimer;
        System.Threading.TimerCallback cb;

        Bitmap m_floorPlanImage;
        Bitmap m_fpBuffer;
        Image m_destImage;
        Image m_robotImage;
        RobotSprite m_robotSprite;
        Position m_robotStart;

        Graphics m_fg;

        Point m_center;
        Point m_destLoc;
        Point m_voidpoint = new Point(-999, -999);
        Point m_scaleStart;
        Point m_scaleEnd;

        short m_leftSpeed = 0;
        short m_rightSpeed = 0;

        public MainForm()
        {
            InitializeComponent();

            m_host = new CRobotHost(this);

            //m_camera = new CWebcam(livePanel, null, false);

            //m_camera.Initialize();
            //m_camera.SetReady();

            m_center = new Point(floorPlanPanel.Width / 2, floorPlanPanel.Height / 2);
            m_destImage = Image.FromFile("destImage.png");
            m_destLoc = m_voidpoint;

            m_robotImage = Image.FromFile("Sprite3.png");
            m_robotStart = null;
            m_robotSprite = null;
            m_status = null;
            m_fg = Graphics.FromHwnd(floorPlanPanel.Handle);
            livePanel.BackgroundImage = Image.FromFile("SampleRobotView.jpg");
            cb = new System.Threading.TimerCallback(DrawFloor);
            m_drawTimer = new Timer(cb, null, 0, 100);

            m_fpState = fpState.NONE;

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

                m_driveTimer = new Timer(Drive, null, 1000, 500);

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


        private void Button1_Click(object sender, EventArgs e)
        {
            m_host.Send(new byte[] { 0x01, 0x10, 0x00, byte.Parse((string)((Button)sender).Tag), 0x00, 0x00, 0x00, 0xEF }, true);
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

        //private void drivePanel_MouseMove(object sender, MouseEventArgs e)
        //{

        //    leftSpeed = 0;
        //    rightSpeed = 0;

        //    if(e.Button == MouseButtons.Left)
        //    {
        //        mouseX = Clamp(e.X, 0, 300);
        //        mouseY = Clamp(e.Y, 0, 300);
        //        ComputeSpeeds();
        //    }

        //    DrawDrivePanel();
        //}

        //private void drivePanel_MouseDown(object sender, MouseEventArgs e)
        //{
        //    mouseX = Clamp(e.X, 0, 300);
        //    mouseY = Clamp(e.Y, 0, 300);
        //    ComputeSpeeds();
        //}

        //private void drivePanel_MouseUp(object sender, MouseEventArgs e)
        //{
        //    mouseX = 150;
        //    mouseY = 150;
        //    leftSpeed = 0;
        //    rightSpeed = 0;
        //}

        //private void ComputeSpeeds()
        //{
        //    double x = mouseX - 150;
        //    double y = 150 - mouseY;

        //    double theta = Math.Acos(y/Length(x, y));//Math.Abs(Math.Atan2(y/Length(x, y), x/Length(x, y)) - Math.Atan2(1, 0));
        //    double speed = Length(x, y)/212 * 1000;

        //    if(x < 0)
        //    {
        //        if(y > 0 || theta > Math.PI*0.75)
        //            leftSpeed = (short)(speed * Math.Sign(y));
        //        else
        //            leftSpeed = (short)(-Math.Sin((theta - Math.PI*0.625) * 4) * speed);

        //        if(theta < Math.PI * 0.25)
        //        {
        //            theta *= 2;
        //            rightSpeed = (short)(Math.Cos(theta) * speed);
        //        }
        //        else if(theta < Math.PI * 0.75)
        //        {
        //            theta -= Math.PI * 0.5;
        //            theta *= 2;
        //            rightSpeed = (short)(Math.Cos(theta) * -speed);
        //        }
        //        else
        //        {
        //            theta -= Math.PI;
        //            theta *= 2;
        //            rightSpeed = (short)(Math.Cos(theta) * -speed);
        //        }
        //    }
        //    else if(x > 0)
        //    {
        //        if(y > 0 || theta > Math.PI*0.75)
        //            rightSpeed = (short)(speed * Math.Sign(y));
        //        else
        //            rightSpeed = (short)(-Math.Sin((theta - Math.PI*0.625) * 4) * speed);

        //        if(theta < Math.PI * 0.25)
        //        {
        //            theta *= 2;
        //            leftSpeed = (short)(Math.Cos(theta) * speed);
        //        }
        //        else if(theta < Math.PI * 0.75)
        //        {
        //            theta -= Math.PI * 0.5;
        //            theta *= 2;
        //            leftSpeed = (short)(Math.Cos(theta) * -speed);
        //        }
        //        else
        //        {
        //            theta -= Math.PI;
        //            theta *= 2;
        //            leftSpeed = (short)(Math.Cos(theta) * -speed);
        //        }
        //    }
        //    else
        //    {
        //        leftSpeed = rightSpeed = (short)(speed * Math.Sign(y));
        //    }
        //}

        private int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            return value;
        }

        private void DrawFloor(object state)
        {
            try
            {
                Bitmap buffer = new Bitmap(floorPlanPanel.Width, floorPlanPanel.Height);
                Graphics bg = Graphics.FromImage(buffer);
                switch (m_fpState)
                {
                    case fpState.NONE:
                        bg.FillRectangle(new SolidBrush(Color.White), 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        break;
                    case fpState.IMAGE:
                        bg.DrawImage(m_floorPlanImage, 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        bg.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Red)), 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        break;
                    case fpState.SCALE:
                        bg.DrawImage(m_floorPlanImage, 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        bg.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Red)), 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        bg.DrawLine(new Pen(Color.Blue), (PointF)m_scaleStart,  floorPlanPanel.PointToClient(System.Windows.Forms.Control.MousePosition));
                        PostMessage("Actual " + System.Windows.Forms.Control.MousePosition);
                        PostMessage("Translated " + floorPlanPanel.PointToClient(System.Windows.Forms.Control.MousePosition));
                        break;
                    case fpState.FLOORPLAN:
                        bg.DrawImage(m_status.floorPlan.toImage(), 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        if (m_robotSprite != null)
                            bg.DrawImage(m_robotSprite.image, m_robotSprite.position.location);
                        break;
                    case fpState.DESTINATION:
                        bg.DrawImage(m_status.floorPlan.toImage(), 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
                        if (m_robotSprite != null)
                            bg.DrawImage(m_robotSprite.image, m_robotSprite.position.location);
                        bg.DrawImage(m_destImage, m_destLoc);
                        break;
                }
                m_fg.DrawImage(buffer, 0, 0);

            }
            catch (InvalidOperationException e) { PostMessage(e.ToString()); };
        }

        private Image DrawMoves()
        {
            Point currentPoint = new Point(m_robotSprite.position.location.X, m_robotSprite.position.location.Y);
            Point oldPoint;
            Bitmap spot = new Bitmap(6, 6);
            Graphics s = Graphics.FromImage(spot);
            s.DrawEllipse(new Pen(Color.Blue), new Rectangle(new Point(0, 0), new Size(3, 3)));
            RobotSprite trailSprite = new RobotSprite(spot, m_robotSprite.position);

            Bitmap trail = new Bitmap(floorPlanPanel.Width, floorPlanPanel.Height);
            s = Graphics.FromImage(trail);

            foreach (MoveCommand cmd in m_status.path)
            {
                oldPoint = currentPoint;
                int[] moves = TranslateMove(cmd);
                trailSprite.move(moves[0], moves[1]);
                currentPoint = new Point(trailSprite.position.location.X, trailSprite.position.location.Y);
                s.DrawLine(new Pen(Color.Blue, 2), oldPoint, currentPoint);
                //s.DrawImage(spot, trailSprite.position.location);
            }
            return trail;
        }

        private void Drive(object state)
        {
            if (m_host.IsConnected)
            {
                byte leftLow = (byte)m_leftSpeed;
                byte leftHigh = (byte)(m_leftSpeed >> 8);

                byte rightLow = (byte)m_rightSpeed;
                byte rightHigh = (byte)(m_rightSpeed >> 8);

                String msg = "\n\r speeds: " + m_leftSpeed + " " + m_rightSpeed;
                PostMessage(msg);
                m_host.Send(new byte[] { 0x01, 0x10, 0x11, leftHigh, leftLow, rightHigh, rightLow, 0xEF }, true);
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
                            m_scaleStart = e.Location;
                            m_fpState = fpState.SCALE;
                            //DrawFloor(null);
                            break;
                        case fpState.SCALE:
                            m_scaleEnd = e.Location;
                            m_fpState = fpState.FLOORPLAN;
                            break;
                        case fpState.FLOORPLAN:
                        case fpState.DESTINATION:
                            SetDestination(sender, e);
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
            PostMessage("Please Click two points on the map whose distance is known");
            m_fpState = fpState.IMAGE;
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
            m_status = new Status(m_floorPlanImage, 3);
            m_robotStart = new Position(new Point(274, 132), 90);
            m_robotSprite = new RobotSprite(m_robotImage, m_robotStart);
            m_fpState = fpState.IMAGE;
            //DrawFloor(null);
        }
		
        private double Length(double x, double y)
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        private void Load_MainForm(object sender, EventArgs e)
        {

        }

        private void Paint_FloorPlanPanel(object sender, PaintEventArgs e)
        {
            //DrawFloor(null);
        }

        private void SetDestination(object sender, MouseEventArgs e)
        {
            m_destLoc = new Point(e.X, e.Y);
            m_fpState = fpState.DESTINATION;
            //DrawFloor(null);
        }

        private int[] TranslateMove(MoveCommand cmd)
        {
            int[] speeds = new int[2];

            if (cmd.direction == MoveCommand.Direction.Forward)
            {
                speeds[0] = speeds[1] = cmd.distance;
            }
            if (cmd.direction == MoveCommand.Direction.CW)
            {
                speeds[1] = cmd.distance;
                speeds[0] = -speeds[1];
            }
            if (cmd.direction == MoveCommand.Direction.CCW)
            {
                speeds[1] = -cmd.distance;
                speeds[0] = -speeds[1];
            }

            return speeds;
        }

	}
}
