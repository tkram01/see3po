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

        private const int HEIGHT = 20;
        private const int WIDTH = 20;
        private RobotSprite sprite;
        private Image floorPlan;
        private Image buffer;
        private Graphics bg;
        private Graphics fg;
        private CRobotClient client;
        //private CServosController servos;
        //private CMotorsController motors;

        public MainForm()
        {
            InitializeComponent();
            client = new CRobotClient(this);
            tryConnect();

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
            if (client.IsConnected)
                client.Disconnect(true);
            else
                tryConnect();
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
                client.Connect(IPAddress.Parse("127.0.0.1"));
        }

        private int BytesToInt(byte high, byte low)
        {
            int value = ((sbyte)high)<< 8; 
            value += low;
            return value;
        }

    }
}
