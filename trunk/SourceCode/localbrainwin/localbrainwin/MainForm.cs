using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
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
        private const byte EOT = 0xEF;
        private const byte IMG_CMD = 0x04;
        private const byte MOTOR_CMD = 0x10;
        private const byte DRIVING_CMD = 0x11;
        private const byte SOUND_CMD = 0x00;
        private const int SERVER_CMD_SIZE = 10;
        private const int MOTOR_PACKET_SIZE = 7;
        private const int SOCKET_PORT1 = 9050;
        private const short FIX_SPEED = 350;
        private const string imagefile = "flower.jpg";
        private const string server_IP = "192.168.2.166";
        short rightSpeed;
        short leftSpeed;
        IPAddress HOSTaddress;
        Boolean debug_model_on = false;

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
            if (debug_model_on)
            {
                if (InvokeRequired)
                {
                    Invoke(new DGuiCallString(PostMessage), message);
                    return;
                }

                messageBox.Text = message + "\r\n" + messageBox.Text;
            }
		}

        private void handle_image_transfer()
        {
            try
            {
                FileStream imgfile = File.OpenRead(imagefile);
                byte[] clientData1 = new byte[imgfile.Length];
                imgfile.Read(clientData1, 0, (int)imgfile.Length);

                Socket clientSock1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSock1.Connect(new IPEndPoint(HOSTaddress, SOCKET_PORT1)); //target machine's ip address and the port number
                clientSock1.Send(clientData1);
                clientSock1.Close();
            }
            catch (Exception ex)
            {
                PostMessage("image request error:" + ex.ToString());
            }

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

                case IMG_CMD:
                    PostMessage("Request image command received from robot host.");
                    handle_image_transfer();
                    client.Send(new byte[] {0x00, IMG_CMD, EOT }, true);
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

		public void HandleMotorsMessage(byte[] rbuffer)
		{
			if(InvokeRequired)
			{
				Invoke(new DGuiCallBuffer(HandleMotorsMessage), rbuffer);
				return;
			}
            byte[] buffer = new byte[rbuffer.Length];
			string msg = "Motors Message Received: ";
            for (int i = 0; i < rbuffer.Length; i++)
            {
                buffer[i] = rbuffer[i];
                msg += buffer[i] + " ";
            }

			PostMessage(msg);

			if(motors.IsConnected)
			{
                if (buffer.Length >= SERVER_CMD_SIZE)
                {
                    byte[] newbuffer = new byte[MOTOR_PACKET_SIZE];
                    for (int i = 1; i < MOTOR_PACKET_SIZE ; i++)
                        newbuffer[i - 1] = buffer[i];
                    newbuffer[MOTOR_PACKET_SIZE - 1] = EOT; // end byte
                    // disable last driving if new driving cmd recieved
                    if (newbuffer[1] == DRIVING_CMD)
                        drivingtimer.Enabled = false; // disable last driving
                    // send same motor command 3 times due to the hardware issue
                    motors.Send(newbuffer);
                    System.Threading.Thread.Sleep(200);
                    motors.Send(newbuffer);
                    
                    if (newbuffer[1] == DRIVING_CMD)
                    {
                        // obtain driving time from command buffer, which are the last 2 bytes before the end byte
                        byte[] B_duration = new byte[2];
                        B_duration[0] = buffer[SERVER_CMD_SIZE - 2];
                        B_duration[1] = buffer[SERVER_CMD_SIZE - 3];
                        ushort duration = BitConverter.ToUInt16(B_duration,0); // driving time (ms)
                        msg = "Duration = ";
                        msg += duration.ToString();
                        PostMessage(msg); 
                        if (duration > 0)
                        {
                            // reset driving time
                            drivingtimer.Interval = duration * 50;
                            drivingtimer.Enabled = true;
                        }
                    }
                    msg = "Message sent: ";
        			for(int i = 0; i < newbuffer.Length; i++)
		    		msg += newbuffer[i] + " ";

			        PostMessage(msg);
                }
			}
		}

		private void remoteConnectMenuItem_Click(object sender, EventArgs e)
		{
            String IPstr;

            if (client.IsConnected)
                client.Disconnect(true);
            else
            {
                try
                {
                    IPstr = IPaddr.Text;
                    HOSTaddress = IPAddress.Parse(IPstr);
                    PostMessage("Host IP is: " + IPstr);
                }
                catch
                {
                    PostMessage("Host IP error!! Will use " + server_IP);
                    IPstr = server_IP;
                    HOSTaddress = IPAddress.Parse(IPstr);
                }
                PostMessage("Connecting to " + IPstr);
                client.Connect(HOSTaddress);
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
            //short speed = FIX_SPEED;
            short lspeed = 0;
            short rspeed = 0;
            byte command = byte.Parse((string)((Button)sender).Tag);

            lspeed = System.Int16.Parse(txtleftspeed.Text);
            rspeed = System.Int16.Parse(txtrightspeed.Text);

            switch (command)
            {
                case 0x01: // forward
                    rightSpeed = rspeed;
                    leftSpeed = lspeed;
                    break;

                case 0x02: // forward left
                    rightSpeed = 0;
                    leftSpeed = lspeed;
                    break;

                case 0x03: // forward right
                    rightSpeed = rspeed;
                    leftSpeed = 0;
                    break;

                case 0x04: // left
                    rightSpeed = (short)-rspeed;
                    leftSpeed = lspeed;
                    break;

                case 0x05: // right
                    rightSpeed = rspeed;
                    leftSpeed = (short)-lspeed;
                    break;

                case 0x06: // backward
                    rightSpeed = (short)-rspeed;
                    leftSpeed = (short)-lspeed;
                    break;

                case 0x07: // backward left
                    rightSpeed = 0;
                    leftSpeed = (short)-lspeed;
                    break;

                case 0x08: // backward right
                    rightSpeed = (short)-rspeed;
                    leftSpeed = 0;
                    break;
            }
            byte leftLow = (byte)leftSpeed;
            byte leftHigh = (byte)(leftSpeed >> 8);
            byte rightLow = (byte)rightSpeed;
            byte rightHigh = (byte)(rightSpeed >> 8);
            byte[] sendb = new byte[] { MOTOR_CMD, DRIVING_CMD, leftHigh, leftLow, rightHigh, rightLow, EOT };
            string msg = "Message sent: ";
            for (int i = 0; i < sendb.Length; i++)
                msg += sendb[i] + " ";

            PostMessage(msg);
            motors.Send(sendb);
            //motors.Send(new byte[] { MOTOR_CMD, DRIVING_CMD , leftHigh, leftLow, rightHigh, rightLow, EOT });
        }

       
        private void button1_Click_1(object sender, EventArgs e)
        {
            //motors.Send(new byte[] { MOTOR_CMD, SOUND_CMD, byte.Parse((string)((Button)sender).Tag), 0x00, 0x00, 0x00, EOT });
            byte[] sendb = new byte[] { MOTOR_CMD, SOUND_CMD, byte.Parse((string)((Button)sender).Tag), 0x00, 0x00, 0x00, EOT };
            string msg = "Message sent: ";
            for (int i = 0; i < sendb.Length; i++)
                msg += sendb[i] + " ";

            PostMessage(msg);
            motors.Send(sendb);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            //rightSpeed = 0;
            //leftSpeed = 0;
            //motors.Send(new byte[] { MOTOR_CMD, DRIVING_CMD, 0, 0, 0, 0, EOT });
            byte[] sendb = new byte[] { MOTOR_CMD, DRIVING_CMD, 0, 0, 0, 0, EOT };
            string msg = "Message sent: ";
            for (int i = 0; i < sendb.Length; i++)
                msg += sendb[i] + " ";

            PostMessage(msg);
            motors.Send(sendb);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            driveButton_Click(turnLeftButton, e);
            System.Threading.Thread.Sleep(200);
            driveButton_Click(turnLeftButton, e);
            movementtimer.Interval = Int32.Parse( T_left90.Text);
            movementtimer.Enabled = true;
        }

        private void movementtimer_Tick(object sender, EventArgs e)
        {
            movementtimer.Enabled = false;
            // send same stop motor command 3 times due to the hardware issue
            stopButton_Click(stopButton, e);
            System.Threading.Thread.Sleep(200);
            stopButton_Click(stopButton, e);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            driveButton_Click(forwardButton, e);
            System.Threading.Thread.Sleep(200);
            driveButton_Click(forwardButton, e);
            movementtimer.Interval = Int32.Parse(T_block .Text);
            movementtimer.Enabled = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            driveButton_Click(turnRightButton, e);
            System.Threading.Thread.Sleep(200);
            driveButton_Click(turnRightButton, e);
            movementtimer.Interval = Int32.Parse(T_right90.Text);
            movementtimer.Enabled = true;
        }

        private void drivingtimer_Tick(object sender, EventArgs e)
        {
            drivingtimer.Enabled = false;
            // send same stop motor command 3 times due to the hardware issue
            stopButton_Click(stopButton, e);
            System.Threading.Thread.Sleep(200);
            stopButton_Click(stopButton, e);
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (debug_model_on)
            {
                menuItem2.Text = "ON";
                debug_model_on = false;
            }
            else
            {
                menuItem2.Text = "OFF";
                debug_model_on = true;
            }
        }


   	}
}
