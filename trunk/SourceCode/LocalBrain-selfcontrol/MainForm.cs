using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using RobotCommands;

namespace LocalBrain
{
   
	public partial class MainForm : Form
	{
		delegate void DGuiCallVoid();
		delegate void DGuiCallString(string str);
		delegate void DGuiCallBuffer(byte[] buffer);

		private CRobotClient client;
		private CServosController servos;
		private CMotorsController motors;
        private const int MOTOR_PACKET_SIZE = 8;
        private const short FIX_SPEED = 350; 
        short rightSpeed;
        short leftSpeed;

        //CRbotoHost host;

		public MainForm()
		{
			InitializeComponent();

			client = new CRobotClient(this);
			servos = new CServosController(this);
			motors = new CMotorsController(this);

			servos.Connect();
			motors.Connect();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if(client.IsConnected)
				client.Disconnect(true);

			if(servos.IsConnected)
				servos.Disconnect();

			if(motors.IsConnected)
				motors.Disconnect();

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

			status += "Robot Status: " + (client.IsConnected ? "Connected" : "Idle") + "     ";
			status += "Servos Status: " + (servos.IsConnected ? "Connected" : "Idle") + "     ";
			status += "Motors Status: " + (motors.IsConnected ? "Connected" : "Idle");

			statusBar.Text = status;

			if(client.IsConnected)
				remoteConnectMenuItem.Text = "Disconnect from Remote Brain";
			else
				remoteConnectMenuItem.Text = "Connect to Remote Brain";

			if(servos.IsConnected)
				servosConnectMenuItem.Text = "Disconnect from Servos";
			else
				servosConnectMenuItem.Text = "Connect to Servos";

			if(motors.IsConnected)
				motorsConnectMenuItem.Text = "Disconnect from Motors";
			else
				motorsConnectMenuItem.Text = "Connect to Motors";
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

			string msg = "System Message Received: ";
			for(int i = 0; i < buffer.Length; i++)
				msg += buffer[i] + " ";

			PostMessage(msg);

			switch(buffer[1])
			{
			    case CRemoteBrainMessage.DISCONNECT:
			        PostMessage("Disconnect message received from robot host.");
			        client.Disconnect(false);
			        break;

			    default:
			        PostMessage("Unknown system message received from robot host.");
			        break;
			}
		}

		public void HandleServosMessage(byte[] buffer)
		{
			if(InvokeRequired)
			{
				Invoke(new DGuiCallBuffer(HandleServosMessage), buffer);
				return;
			}

			string msg = "Servos Message Received: ";
			for(int i = 0; i < buffer.Length; i++)
				msg += buffer[i] + " ";

			PostMessage(msg);

			if(servos.IsConnected)
			{
				byte[] newbuffer = new byte[buffer.Length-1];
				for(int i = 1; i < buffer.Length; i++)
					newbuffer[i-1] = buffer[i];

				servos.Send(newbuffer);
			}
		}

		public void HandleMotorsMessage(byte[] buffer)
		{
			if(InvokeRequired)
			{
				Invoke(new DGuiCallBuffer(HandleMotorsMessage), buffer);
				return;
			}

			string msg = "Motors Message Received: ";
			for(int i = 0; i < buffer.Length; i++)
				msg += buffer[i] + " ";

			PostMessage(msg);

			if(motors.IsConnected)
			{               
                if (buffer.Length >= MOTOR_PACKET_SIZE)
                {
                    byte[] newbuffer = new byte[MOTOR_PACKET_SIZE - 1];
                    for (int i = 1; i < MOTOR_PACKET_SIZE; i++)
                        newbuffer[i - 1] = buffer[i];

                    motors.Send(newbuffer);
                }
			}
		}

		private void remoteConnectMenuItem_Click(object sender, EventArgs e)
		{
            IPAddress address;
            String IPstr;

            if (client.IsConnected)
                client.Disconnect(true);
            else
            {
                try
                {
                    IPstr = IPaddr.Text;
                    address = IPAddress.Parse(IPstr);
                    PostMessage("Host IP is: " + IPstr);
                }
                catch
                {
                    PostMessage("Host IP error!! Use 192.168.2.166");
                    IPstr ="192.168.2.166";
                    address = IPAddress.Parse(IPstr);
                }
                PostMessage("Connecting to " + IPstr);
                client.Connect(address);
            }
		}

		private void servosConnectMenuItem_Click(object sender, EventArgs e)
		{
			if(servos.IsConnected)
				servos.Disconnect();
			else
				servos.Connect();
		}

		private void motorsConnectMenuItem_Click(object sender, EventArgs e)
		{
			if(motors.IsConnected)
				motors.Disconnect();
			else
				motors.Connect();
		}

		private void exitMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}


        protected void driveButton_Click(object sender, EventArgs e)
        {
            //short speed = (short)speedBar.Value;
            short speed = FIX_SPEED;
            byte command = byte.Parse((string)((Button)sender).Tag);
            

            switch (command)
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
            byte leftLow = (byte)leftSpeed;
            byte leftHigh = (byte)(leftSpeed >> 8);
            byte rightLow = (byte)rightSpeed;
            byte rightHigh = (byte)(rightSpeed >> 8);
            motors.Send(new byte[] { 0x10, 0x11, leftHigh, leftLow, rightHigh, rightLow, 0xEF });
        }

       
        private void button1_Click_1(object sender, EventArgs e)
        {
            motors.Send(new byte[] { 0x10, 0x00, byte.Parse((string)((Button)sender).Tag), 0x00, 0x00, 0x00, 0xEF } );
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            //rightSpeed = 0;
            //leftSpeed = 0;
            motors.Send(new byte[] { 0x10, 0x11, 0, 0, 0, 0, 0xEF });
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            driveButton_Click(turnLeftButton, e);
            movementtimer.Interval = Int32.Parse( T_left90.Text);
            movementtimer.Enabled = true;
        }

        private void movementtimer_Tick(object sender, EventArgs e)
        {
            movementtimer.Enabled = false;
            stopButton_Click(stopButton, e);
            System.Threading.Thread.Sleep(50);
            stopButton_Click(stopButton, e);
            System.Threading.Thread.Sleep(50);
            stopButton_Click(stopButton, e);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            driveButton_Click(forwardButton, e);
            movementtimer.Interval = Int32.Parse(T_block .Text);
            movementtimer.Enabled = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            driveButton_Click(turnRightButton, e);
            movementtimer.Interval = Int32.Parse(T_right90.Text);
            movementtimer.Enabled = true;
        }

   	}
}
