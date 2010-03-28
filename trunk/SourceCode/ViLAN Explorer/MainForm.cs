using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Timer = System.Threading.Timer;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Windows.Media.Imaging;

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
        Thread t1;
        int flag = 0;
        string receivedPath = @"my pic.jpg";
        Queue<byte> data = new Queue<byte>();
        public delegate void MyDelegate();


		CRobotHost host;
		CWebcam camera;
		Timer driveTimer;
		short rightSpeed;
		short leftSpeed;
        Bitmap pic;

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

				//driveTimer = new Timer(Drive, null, 1000, 500);

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

			host.Send(new byte[] {0x01, 0x10, 0x11, leftHigh, leftLow, rightHigh, rightLow,0,0, 0xEF}, true);
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

                case 4:
                    PostMessage("Image recieved.");
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
            host.Send(new byte[] { 0x01, 0x10, 0x00, byte.Parse((string)((Button)sender).Tag), 0x00, 0x00, 0x00,0,0, 0xEF }, true);
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
            Drive(null);
		}

		private void backRightButton_MouseUp(object sender, MouseEventArgs e)
		{
			rightSpeed = 0;
			leftSpeed = 0;
            Drive(null);
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

        public void StartListening()
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
                label1.Tag = ex.ToString();
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

            //int fileNameLen = 1;
            String content = String.Empty;
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            int bytesRead = handler.EndReceive(ar);
            if (bytesRead > 0)
            {

                if (flag == 0)
                {
                    //fileNameLen = BitConverter.ToInt32(state.buffer, 0);
                    //string fileName = Encoding.UTF8.GetString(state.buffer, 4, fileNameLen);
                    //receivedPath = @"C:\temp\" + fileName;
                    for (int i = 0; i < bytesRead; i++)
                    {
                        data.Enqueue(state.buffer[i]);
                    }

                    flag++;
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
                }
                else
                //if (flag >= 1)
                {
                    //BinaryWriter writer = new BinaryWriter(File.Open(receivedPath, FileMode.Append));
                    //if (flag == 1)
                    //{
                    //    writer.Write(state.buffer, 4 + fileNameLen, bytesRead - (4 + fileNameLen));
                    //    flag++;
                    //}
                    //else
                    //    writer.Write(state.buffer, 0, bytesRead);
                    //writer.Close();
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
                Invoke(new MyDelegate(LabelWriter));
            }

        }

        public void LabelWriter()
        {
            label1.Text = "Data has been received";
            PictureBox mypicbox = new PictureBox();
            mypicbox.Image = pic;
            mypicbox.Anchor = AnchorStyles.Top;
            mypicbox.SizeMode = PictureBoxSizeMode.AutoSize;
            livePanel.Controls.Add(mypicbox);
            livePanel.Show();
        }



        private void btCamera_Click(object sender, EventArgs e)
        {
            t1 = new Thread(new ThreadStart(StartListening));
            t1.Start();
            host.Send(new byte[] { 0, 4, 0xEF }, true);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            t1.Abort();
        }
	}
}
