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

namespace ViLAN_Explorer
{
	public partial class MainForm : Form, IRobotParent
	{
		delegate void DGuiCallVoid();
		delegate void DGuiCallString(string str);
		delegate void DGuiCallBuffer(byte[] buffer);

		CRobotHost host;
		CWebcam camera;
		Timer driveTimer;
		Bitmap driveBackground;
		Graphics g;

		short leftSpeed = 0;
		short rightSpeed = 0;
		int mouseX = 150;
		int mouseY = 150;

		public MainForm()
		{
			InitializeComponent();

			host = new CRobotHost(this);
			camera = new CWebcam(livePanel, null, false);
			camera.Initialize();
			camera.SetReady();

			driveBackground = new Bitmap("driveBackground2.png");
			g = Graphics.FromHwnd(drivePanel.Handle);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			DrawDrivePanel();

			base.OnPaint(e);
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if(host.IsListening)
				host.StopListening();
			else if(host.IsConnected)
				host.Disconnect(true);

			base.OnClosing(e);
		}

		void drivePanel_Paint(object sender, PaintEventArgs e)
		{
			DrawDrivePanel();
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

		private void drivePanel_MouseMove(object sender, MouseEventArgs e)
		{
		    mouseX = 150;
		    mouseY = 150;
			leftSpeed = 0;
			rightSpeed = 0;

			if(e.Button == MouseButtons.Left)
			{
				mouseX = Clamp(e.X, 0, 300);
				mouseY = Clamp(e.Y, 0, 300);
				ComputeSpeeds();
			}

			DrawDrivePanel();
		}

		private void drivePanel_MouseDown(object sender, MouseEventArgs e)
		{
			mouseX = Clamp(e.X, 0, 300);
			mouseY = Clamp(e.Y, 0, 300);
			ComputeSpeeds();
		}

		private void drivePanel_MouseUp(object sender, MouseEventArgs e)
		{
		    mouseX = 150;
		    mouseY = 150;
			leftSpeed = 0;
			rightSpeed = 0;
		}

		private void ComputeSpeeds()
		{
		    double x = mouseX - 150;
		    double y = 150 - mouseY;

		    double theta = Math.Acos(y/Length(x, y));//Math.Abs(Math.Atan2(y/Length(x, y), x/Length(x, y)) - Math.Atan2(1, 0));
		    double speed = Length(x, y)/212 * 1000;

			if(x < 0)
			{
				if(y > 0 || theta > Math.PI*0.75)
					leftSpeed = (short)(speed * Math.Sign(y));
				else
					leftSpeed = (short)(-Math.Sin((theta - Math.PI*0.625) * 4) * speed);

				if(theta < Math.PI * 0.25)
				{
					theta *= 2;
					rightSpeed = (short)(Math.Cos(theta) * speed);
				}
				else if(theta < Math.PI * 0.75)
				{
					theta -= Math.PI * 0.5;
					theta *= 2;
					rightSpeed = (short)(Math.Cos(theta) * -speed);
				}
				else
				{
					theta -= Math.PI;
					theta *= 2;
					rightSpeed = (short)(Math.Cos(theta) * -speed);
				}
			}
			else if(x > 0)
			{
				if(y > 0 || theta > Math.PI*0.75)
					rightSpeed = (short)(speed * Math.Sign(y));
				else
					rightSpeed = (short)(-Math.Sin((theta - Math.PI*0.625) * 4) * speed);

				if(theta < Math.PI * 0.25)
				{
					theta *= 2;
					leftSpeed = (short)(Math.Cos(theta) * speed);
				}
				else if(theta < Math.PI * 0.75)
				{
					theta -= Math.PI * 0.5;
					theta *= 2;
					leftSpeed = (short)(Math.Cos(theta) * -speed);
				}
				else
				{
					theta -= Math.PI;
					theta *= 2;
					leftSpeed = (short)(Math.Cos(theta) * -speed);
				}
			}
			else
			{
				leftSpeed = rightSpeed = (short)(speed * Math.Sign(y));
			}
		}

		void DrawDrivePanel()
		{
			g.DrawImage(driveBackground, 0, 0, 300, 300);
			g.DrawLine(Pens.Black, 150, 150, mouseX, mouseY);
		}

		public void Drive(object state)
		{
			if(host.IsConnected)
			{
				byte leftLow = (byte)leftSpeed;
				byte leftHigh = (byte)(leftSpeed >> 8);

				byte rightLow = (byte)rightSpeed;
				byte rightHigh = (byte)(rightSpeed >> 8);

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
	}
}
