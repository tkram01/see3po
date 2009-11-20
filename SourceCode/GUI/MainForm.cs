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

using Nexus3Input;
using RobotCommands;
using RobotHost;

namespace See3PO
{
	public partial class MainForm : Form, IRobotParent
	{
        Point voidPoint = new Point(-999, -999);
		delegate void DGuiCallVoid();
		delegate void DGuiCallString(string str);
		delegate void DGuiCallBuffer(byte[] buffer);

        Status status;

		CRobotHost host;
		CWebcam camera;
		Timer driveTimer;
		Bitmap floorPlanImage;
        Bitmap fpBuffer;

        Image destImage;
        Image robotImage;
        RobotSprite robotSprite;
        Position robotStart;

		Graphics g;
        Graphics fb;

        Point Center;
        Point destLoc;

		short leftSpeed = 0;
		short rightSpeed = 0;

		public MainForm()
		{

			InitializeComponent();

			host = new CRobotHost(this);

            //camera = new CWebcam(livePanel, null, false);

			//camera.Initialize();
			//camera.SetReady();

            Center = new Point(floorPlanPanel.Width/2, floorPlanPanel.Height/2);
            destImage = Image.FromFile("destImage.png");
            destLoc = voidPoint;

            robotImage = Image.FromFile("Sprite3.png");
            robotStart = null;
            robotSprite = null;


            g = Graphics.FromHwnd(floorPlanPanel.Handle);
            livePanel.BackgroundImage = Image.FromFile("SampleRobotView.jpg");

            //status.path = new Queue<MoveCommand>();

            
		}

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    DrawFloor();
        //    base.OnPaint(e);
        //}

		protected override void OnClosing(CancelEventArgs e)
		{
			if(host.IsListening)
				host.StopListening();
			else if(host.IsConnected)
				host.Disconnect(true);

			base.OnClosing(e);
		}

		void floorPlanPanel_Paint(object sender, PaintEventArgs e)
		{
			DrawFloor();
		}

		private void connectMenuItem_Click(object sender, EventArgs e)
		{
			if(host.IsListening)
				host.StopListening();
			else if(host.IsConnected)
				host.Disconnect(true);
			else
				host.StartListening();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			host.Send(new byte[] {0x01, 0x10, 0x00, byte.Parse((string)((Button)sender).Tag), 0x00, 0x00, 0x00, 0xEF}, true);
		}


        private void drawDestination(object sender, MouseEventArgs e)
        {
            destLoc = new Point(e.X, e.Y);
            PostMessage(e.X + "," + e.Y + "\r\n");
            DrawFloor();

        }

        private void setDestination(object sender, MouseEventArgs e)
        {
            drawDestination(sender, e);
            DrawFloor();
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

		private void DrawFloor()
		{
            if (floorPlanImage != null)
                g.DrawImage(floorPlanImage, 0, 0, floorPlanPanel.Width, floorPlanPanel.Height);
            if (robotSprite != null)
                g.DrawImage(robotSprite.image, robotSprite.position.location);
            if (destLoc != voidPoint)
                g.DrawImage(destImage, destLoc);

		}

        private Image DrawMoves() {
            Point currentPoint = new Point(robotSprite.position.location.X, robotSprite.position.location.Y);
            Point oldPoint;
            Bitmap spot = new Bitmap(6, 6);
            Graphics s = Graphics.FromImage(spot);
            s.DrawEllipse(new Pen(Color.Blue),new Rectangle(new Point(0,0), new Size(3, 3)));
            RobotSprite trailSprite = new RobotSprite(spot, robotSprite.position);

            Bitmap trail = new Bitmap(floorPlanPanel.Width, floorPlanPanel.Height);
            s = Graphics.FromImage(trail);

            foreach (MoveCommand cmd in status.path){
                oldPoint = currentPoint;
                int[] moves = translateMove(cmd);
                trailSprite.move(moves[0], moves[1]);
                currentPoint = new Point(trailSprite.position.location.X, trailSprite.position.location.Y);
                s.DrawLine(new Pen(Color.Blue, 2), oldPoint, currentPoint);
                //s.DrawImage(spot, trailSprite.position.location);
            }
            return trail;
        }

		public void Drive(object state)
		{
			if(host.IsConnected)
			{
				byte leftLow = (byte)leftSpeed;
				byte leftHigh = (byte)(leftSpeed >> 8);

				byte rightLow = (byte)rightSpeed;
				byte rightHigh = (byte)(rightSpeed >> 8);

                String msg = "\n\r speeds: " + leftSpeed + " " + rightSpeed;
                PostMessage(msg);
				host.Send(new byte[] {0x01, 0x10, 0x11, leftHigh, leftLow, rightHigh, rightLow, 0xEF}, true);
			}
		}

		public void UpdateStatus()
		{
			if(InvokeRequired)
			{
				Invoke(new DGuiCallVoid(UpdateStatus));
				return;
			}

			string status = "";

			if(host.IsListening)
			{
				status += "Robot Status: Listening";
				connectMenuItem.Text = "Stop Listening";
			}
			else if(host.IsConnected)
			{
				status += "Robot Status: Connected";
				connectMenuItem.Text = "Disconnect";

				driveTimer = new Timer(Drive, null, 1000, 500);

				host.Send((char)CRemoteBrainMessage.SERVO + "#16 P1500 #17 P1500 #18 P1500 #19 P1500 #20 P 1500\r", true);
			}
			else
			{
				status += "Robot Status: Idle";
				connectMenuItem.Text = "Listen";

				if(driveTimer != null)
				{
					driveTimer.Dispose();
					driveTimer = null;
				}
			}

			statusLabel.Text = status;
		}

		public void HandleSystemMessage(byte[] buffer)
		{
			if(InvokeRequired)
			{
				Invoke(new DGuiCallBuffer(HandleSystemMessage), buffer);
				return;
			}

			switch(buffer[1])
			{
				case CLocalBrainMessage.DISCONNECT:
					PostMessage("Client disconnect message received.");
					host.Disconnect(false);
					break;

				default:
					PostMessage("Unknown system message received from robot client.");
					break;
			}
		}

		public void HandleDataMessage(byte[] buffer)
		{
			if(InvokeRequired)
			{
				Invoke(new DGuiCallBuffer(HandleDataMessage), buffer);
				return;
			}
		}

		public void PostMessage(string message)
		{
			if(InvokeRequired)
			{
				Invoke(new DGuiCallString(PostMessage), message);
				return;
			}

			messageBox.Text = message + "\r\n" + messageBox.Text;
		}

		private double Length(double x, double y)
		{
			return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
		}

		private int Clamp(int value, int min, int max)
		{
			if(value < min)
				return min;
			else if(value > max)
				return max;

			return value;
		}

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void Import_Click(object sender, EventArgs e)
        {
            importImageDialog.InitialDirectory = ".\floorplan";
            importImageDialog.ShowDialog();
            try
            {
                floorPlanImage = new Bitmap(Image.FromFile(importImageDialog.FileName));
            }
            catch (Exception) { }
            status = new Status(floorPlanImage, 3);
            robotStart = new Position(new Point(274, 132), 90);
            robotSprite = new RobotSprite(robotImage, robotStart);
            DrawFloor();
        }

        private int[] translateMove(MoveCommand cmd){
            int [] speeds = new int [2];
            
            if (cmd.direction == MoveCommand.Direction.Forward){
                speeds[0] = speeds[1] = cmd.distance;
            }
            if (cmd.direction == MoveCommand.Direction.CW){
                speeds[1] = cmd.distance;
                speeds[0] = -speeds[1];
            }
            if (cmd.direction == MoveCommand.Direction.CCW){
                speeds[1] = -cmd.distance;
                speeds[0] = -speeds[1];
            }

            return speeds;
        }

	}
}
