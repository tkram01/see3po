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
		short rightSpeed;
		short leftSpeed;

		public MainForm()
		{
			InitializeComponent();

			host = new CRobotHost(this);
			camera = new CWebcam(livePanel, null);
			camera.Initialize();
			camera.SetReady();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if(host.IsListening)
				host.StopListening();
			else if(host.IsConnected)
				host.Disconnect(true);

			base.OnClosing(e);
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

		public void Drive(object state)
		{
			byte leftLow = (byte)leftSpeed;
			byte leftHigh = (byte)(leftSpeed >> 8);
			byte rightLow = (byte)rightSpeed;
			byte rightHigh = (byte)(rightSpeed >> 8);

			host.Send(new byte[] {0x01, 0x10, 0x11, leftHigh, leftLow, rightHigh, rightLow, 0xEF}, true);
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

		private void button1_Click(object sender, EventArgs e)
		{
			host.Send(new byte[] {0x01, 0x10, 0x00, byte.Parse((string)((Button)sender).Tag), 0x00, 0x00, 0xEF}, true);
		}

		private void driveButton_Click(object sender, MouseEventArgs e)
		{
			short speed = (short)speedBar.Value;
			byte command = byte.Parse((string)((Button)sender).Tag);

			switch(command)
			{
			case 0x01:
				rightSpeed = speed;
				leftSpeed = speed;
				break;

			case 0x02:
				rightSpeed = 0;
				leftSpeed = speed;
				break;

			case 0x03:
				rightSpeed = speed;
				leftSpeed = 0;
				break;

			case 0x04:
				rightSpeed = (short)-speed;
				leftSpeed = speed;
				break;

			case 0x05:
				rightSpeed = speed;
				leftSpeed = (short)-speed;
				break;

			case 0x06:
				rightSpeed = (short)-speed;
				leftSpeed = (short)-speed;
				break;

			case 0x07:
				rightSpeed = 0;
				leftSpeed = (short)-speed;
				break;

			case 0x08:
				rightSpeed = (short)-speed;
				leftSpeed = 0;
				break;
			}
		}

		private void backRightButton_MouseUp(object sender, MouseEventArgs e)
		{
			rightSpeed = 0;
			leftSpeed = 0;
		}
	}
}
