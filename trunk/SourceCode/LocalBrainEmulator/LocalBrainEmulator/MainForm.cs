using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using RobotCommands;

namespace LocalBrainEmulator
{
    public partial class MainForm : Form
    {
        private const int HEIGHT = 20;
        private const int WIDTH = 20;

        delegate void DGuiCallVoid();
        delegate void DGuiCallString(string str);
        delegate void DGuiCallBuffer(byte[] buffer);

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

            buffer = new Bitmap(livePanel.Width, livePanel.Height);
            fg = Graphics.FromHwnd(livePanel.Handle);
            bg = Graphics.FromImage(buffer);

            floorPlan = new Bitmap("testFloor.jpg");
            livePanel.BackgroundImage = floorPlan;
            Image spriteImage = new Bitmap("sprite3.png");
            sprite = new RobotSprite(new Bitmap(spriteImage, HEIGHT, WIDTH), new Point(livePanel.Width/2, livePanel.Height/2));

            drawToBuffer();
            drawFromBuffer();

            //livePanel.draw

            client = new CRobotClient(this);
            //servos = new CServosController(this);
            //motors = new CMotorsController(this);

            //servos.Connect();
            //motors.Connect();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (client.IsConnected)
                client.Disconnect(true);

            //if (servos.IsConnected)
            //    servos.Disconnect();

            //if (motors.IsConnected)
            //    motors.Disconnect();

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
            //status += "Servos Status: " + (servos.IsConnected ? "Connected" : "Idle") + "     ";
            //status += "Motors Status: " + (motors.IsConnected ? "Connected" : "Idle");

            statusBar.Text = status;

            //if (client.IsConnected)
            //    remoteConnectMenuItem.Text = "Disconnect from Remote Brain";
            //else
            //    remoteConnectMenuItem.Text = "Connect to Remote Brain";

            //if (servos.IsConnected)
            //    servosConnectMenuItem.Text = "Disconnect from Servos";
            //else
            //    servosConnectMenuItem.Text = "Connect to Servos";

            //if (motors.IsConnected)
            //    motorsConnectMenuItem.Text = "Disconnect from Motors";
            //else
            //    motorsConnectMenuItem.Text = "Connect to Motors";
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
                    break;

                default:
                    PostMessage("Unknown system message received from robot host.");
                    break;
            }
        }

        public void HandleServosMessage(byte[] buffer)
        {
            //if (InvokeRequired)
            //{
            //    Invoke(new DGuiCallBuffer(HandleServosMessage), buffer);
            //    return;
            //}

            //string msg = "Servos Message Received: ";
            //for (int i = 0; i < buffer.Length; i++)
            //    msg += buffer[i] + " ";

            //PostMessage(msg);

            //if (servos.IsConnected)
            //{
            //    byte[] newbuffer = new byte[buffer.Length - 1];
            //    for (int i = 1; i < buffer.Length; i++)
            //        newbuffer[i - 1] = buffer[i];

            //    servos.Send(newbuffer);
            //}
        }

        public void HandleMotorsMessage(byte[] buffer)
        {
            int leftSpeed = buffer[3] < 128 ? buffer[3] : buffer[3] - 255;
            int rightSpeed = buffer[5] < 128 ? buffer[5] : buffer[5] - 255;

            string msg = "Motors Message Received: ";
            for (int i = 0; i < buffer.Length; i++)
                msg += buffer[i] + "\n\r ";
            PostMessage(msg);

            msg =  "\n\r speeds: " + leftSpeed + " " + rightSpeed;
            PostMessage(msg);

            if (leftSpeed != 0 || rightSpeed != 0)
            {
                sprite.move(leftSpeed, rightSpeed);
                drawToBuffer();
                drawFromBuffer();
            }
            msg = "\n\r position: " + sprite.position;
            msg += "\n\r facing: " + sprite.facing;
            PostMessage(msg);
        }

        private void remoteConnectMenuItem_Click(object sender, EventArgs e)
        {
            if (client.IsConnected)
                client.Disconnect(true);
            else
                client.Connect(IPAddress.Parse("127.0.0.1"));
        }

        private void servosConnectMenuItem_Click(object sender, EventArgs e)
        {
            //if (servos.IsConnected)
            //    servos.Disconnect();
            //else
            //    servos.Connect();
        }

        private void motorsConnectMenuItem_Click(object sender, EventArgs e)
        {
            //if (motors.IsConnected)
            //    motors.Disconnect();
            //else
            //    motors.Connect();
        }

        private void drawFromBuffer()
        {
            fg.DrawImage(buffer, new Point(0, 0));
        }

        private void drawToBuffer()
        {
            bg.DrawImage(floorPlan, 0, 0, livePanel.Width, livePanel.Height);
            bg.DrawImage(sprite.image, sprite.position.X, sprite.position.Y, WIDTH, HEIGHT);
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

        }

    }
}
