using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RobotCommands;

namespace Remote_Servo_Controller
{
	public partial class MainForm : Form
	{
		CRobotHost host;

		delegate void DGuiCallVoid();
		delegate void DGuiCallString(string str);
		delegate void DGuiCallBuffer(byte[] buffer);

		public MainForm()
		{
			InitializeComponent();

			host = new CRobotHost(this);
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if(host.IsListening)
				host.StopListening();
			else if(host.IsConnected)
				host.Disconnect(true);

			base.OnClosing(e);
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

				host.Send((char)CRemoteBrainMessage.SERVO + "#16 P1500 #17 P1500 #18 P1500 #19 P1500 #20 P 1500\r", true);
			}
			else
			{
				status += "Robot Status: Idle";
				connectMenuItem.Text = "Listen";
			}

			statusLabel.Text = status;
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

		private void connectMenuItem_Click(object sender, EventArgs e)
		{
			if(host.IsListening)
				host.StopListening();
			else if(host.IsConnected)
				host.Disconnect(true);
			else
				host.StartListening();
		}

		private void exitMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void up16_Click(object sender, EventArgs e)
		{
			if(bar16.Value + 25 <= bar16.Maximum)
				bar16.Value += 25;
		}

		private void down16_Click(object sender, EventArgs e)
		{
			if(bar16.Value - 25 >= bar16.Minimum)
				bar16.Value -= 25;
		}

		private void bar16_ValueChanged_1(object sender, EventArgs e)
		{
			if(host.IsConnected)
				host.Send((char)CRemoteBrainMessage.SERVO + "#16 P" + bar16.Value + "\r", true);
			else if(bar16.Value != 1500)
				bar16.Value = 1500;
		}

		private void left17_Click(object sender, EventArgs e)
		{
			if(bar17.Value - 25 >= bar17.Minimum)
				bar17.Value -= 25;
		}

		private void right17_Click(object sender, EventArgs e)
		{
			if(bar17.Value + 25 <= bar17.Maximum)
				bar17.Value += 25;
		}

		private void bar17_ValueChanged(object sender, EventArgs e)
		{
			if(host.IsConnected)
				host.Send((char)CRemoteBrainMessage.SERVO + "#17 P" + bar17.Value + "\r", true);
			else if(bar17.Value != 1500)
				bar17.Value = 1500;
		}

		private void left18_Click(object sender, EventArgs e)
		{
			if(bar18.Value - 25 >= bar18.Minimum)
				bar18.Value -= 25;
		}

		private void right18_Click(object sender, EventArgs e)
		{
			if(bar18.Value + 25 <= bar18.Maximum)
				bar18.Value += 25;
		}

		private void bar18_ValueChanged(object sender, EventArgs e)
		{
			if(host.IsConnected)
				host.Send((char)CRemoteBrainMessage.SERVO + "#18 P" + bar18.Value + "\r", true);
			else if(bar18.Value != 1500)
				bar18.Value = 1500;
		}

		private void left19_Click(object sender, EventArgs e)
		{
			if(bar19.Value - 25 >= bar19.Minimum)
				bar19.Value -= 25;
		}

		private void right19_Click(object sender, EventArgs e)
		{
			if(bar19.Value + 25 <= bar19.Maximum)
				bar19.Value += 25;
		}

		private void bar19_ValueChanged(object sender, EventArgs e)
		{
			if(host.IsConnected)
				host.Send((char)CRemoteBrainMessage.SERVO + "#19 P" + bar19.Value + "\r", true);
			else if(bar19.Value != 1500)
				bar19.Value = 1500;
		}

		private void down20_Click(object sender, EventArgs e)
		{
			if(bar20.Value - 25 >= bar20.Minimum)
				bar20.Value -= 25;
		}

		private void up20_Click(object sender, EventArgs e)
		{
			if(bar20.Value + 25 <= bar20.Maximum)
				bar20.Value += 25;
		}

		private void bar20_ValueChanged(object sender, EventArgs e)
		{
			if(host.IsConnected)
				host.Send((char)CRemoteBrainMessage.SERVO + "#20 P" + (bar20.Maximum - bar20.Value + bar20.Minimum) + "\r", true);
			else if(bar20.Value != 1500)
				bar20.Value = 1500;
		}

		private void setServoPositionMenuItem_Click(object sender, EventArgs e)
		{
			ServoPositionDialog dialog = new ServoPositionDialog();

			if(dialog.ShowDialog() == DialogResult.OK)
				host.Send((char)CRemoteBrainMessage.SERVO + "#" + dialog.ServoNumber + " P" + dialog.Position + "\r", true);
		}

		private void centerAllMenuItem_Click(object sender, EventArgs e)
		{
			bar16.Value = 1500;
			bar17.Value = 1500;
			bar18.Value = 1500;
			bar19.Value = 1500;
			bar20.Value = 1500;
		}
	}
}