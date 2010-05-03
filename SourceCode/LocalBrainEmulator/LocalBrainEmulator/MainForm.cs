using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using Timer = System.Threading.Timer;

using RobotCommands;
using System.IO;
using System.Net.Sockets;

namespace LocalBrainEmulator
{
    public partial class MainForm : Form
    {
        delegate void DMove();
        Timer t_MoveTimer;
        TimerCallback t_moveCallback;

        delegate void DGuiCallVoid();
        delegate void DGuiCallString(string str);
        delegate void DGuiCallBuffer(byte[] buffer);

        private const byte EOT = 0xEF;
        private const byte IMG_CMD = 0x04;
        private const byte MOTOR_CMD = 0x10;
        private const byte DRIVING_CMD = 0x11;
        private const byte SOUND_CMD = 0x00;
        private const int SERVER_CMD_SIZE = 10;
        private const int MOTOR_PACKET_SIZE = 7;
        private const int SOCKET_PORT1 = 9050;
        private const short FIX_SPEED = 350;
        private const string imagefile = ".\\sendImages\\flower";
        private const string server_IP = "127.0.0.1";
        private CRobotClient client;
        IPAddress HOSTaddress;
        //private CServosController servos;
        //private CMotorsController motors;

        int imagecounter;

        public MainForm()
        {
            InitializeComponent();
            client = new CRobotClient(this);
            tryConnect();
            imagecounter = 0;

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (client.IsConnected)
                client.Disconnect(true);

            base.OnClosing(e);
        }

        public void UpdateStatus()
        {
            if (InvokeRequired)
            {
                Invoke(new DGuiCallVoid(UpdateStatus));
                return;
            }

            string status = "";

            status += "Robot Status: " + (client.IsConnected ? "Connected" : "Idle") + "     ";

            statusBar.Text = status;
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

        public void HandleSystemMessage(byte[] buffer)
        {
            if (InvokeRequired)
            {
                Invoke(new DGuiCallBuffer(HandleSystemMessage), buffer);
                return;
            }

            string msg = "System Message Received: ";
            for (int i = 0; i < buffer.Length; i++)
                msg += buffer[i] + " ";

            PostMessage(msg);

            switch (buffer[1])
            {
                case CRemoteBrainMessage.DISCONNECT:
                    PostMessage("Disconnect message received from robot host.");
                    client.Disconnect(false);
                    tryConnect();
                    break;

                case IMG_CMD:
                    PostMessage("Request image command received from robot host.");
                    handle_image_transfer();
                    client.Send(new byte[] { 0x00, IMG_CMD, EOT }, true);
                    break;

                default:
                    PostMessage("Unknown system message received from robot host.");
                    if (!client.IsConnected)
                        tryConnect();
                    break;
            }
        }

        public void HandleServosMessage(byte[] buffer)
        {
        }


        public void HandleMotorsMessage(byte[] buffer)
        {
            if (InvokeRequired)
            {
                Invoke(new DGuiCallBuffer(HandleMotorsMessage), buffer);
                return;
            }
            byte[] B_duration = new byte[2];
            B_duration[0] = buffer[3];
            B_duration[1] = buffer[4];
            short LeftSpeed = (short)(BitConverter.ToInt16(B_duration, 0)); // driving time (ms)

            B_duration = new byte[2];
            B_duration[0] = buffer[5];
            B_duration[1] = buffer[6];
            short RightSpeed = (short)(BitConverter.ToInt16(B_duration, 0)); // driving time (ms)

            B_duration = new byte[2];
            B_duration[0] = buffer[7];
            B_duration[1] = buffer[8];
            ushort Duration = (ushort)(BitConverter.ToUInt16(B_duration, 0) * 50); // driving time (ms)


            string msg = "Motors Message Received: ";

            for (int i = 0; i < buffer.Length; i++)
                msg += buffer[i] + "\n\r ";
            PostMessage(msg);

            PostMessage( "\n\r speeds: " + LeftSpeed + " " + RightSpeed);
            PostMessage( "\n\r duration: " + Duration);

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
                    IPstr = server_IP; //IPaddr.Text;
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
        }

        private void motorsConnectMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void exitMenuItem(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tryConnect() 
        {
            while (!client.IsConnected)
                remoteConnectMenuItem_Click(this, null);
        }

        private int BytesToInt(byte high, byte low)
        {
            int value = ((sbyte)high)<< 8; 
            value += low;
            return value;
        }

        private void handle_image_transfer()
        {

            try
            {
                imagecounter = (imagecounter + 1) % 3;
                FileStream imgfile = File.OpenRead(imagefile + imagecounter + ".jpg");
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

    }
}
