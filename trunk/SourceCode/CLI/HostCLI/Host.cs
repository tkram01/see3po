using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostCLI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Timer = System.Threading.Timer;

    using See3PO;
    using RobotCommands;
    using RobotHost;

    class Host : IRobotParent
    {
        char[] splitchar = { ' ' };

        delegate void DGuiCallVoid();
        delegate void DGuiCallString(string str);
        delegate void DGuiCallBuffer(byte[] buffer);

        CRobotHost host;
        Timer driveTimer;
        byte rightSpeed;
        byte leftSpeed;

        public Host()
        {
            host = new CRobotHost(this);
        }

        public void connect()
        {
            if (host.IsListening)
                host.StopListening();
            else if (host.IsConnected)
                host.Disconnect(true);
            else
                host.StartListening();
        }

        public void UpdateStatus()
        {
            string status = "";

            if (host.IsListening)
            {
                status += "Robot Status: Listening";
            }
            else if (host.IsConnected)
            {
                status += "Robot Status: Connected";

                driveTimer = new Timer(Drive, null, 1000, 500);

                host.Send((char)CRemoteBrainMessage.SERVO + "#16 P1500 #17 P1500 #18 P1500 #19 P1500 #20 P 1500\r", true);
            }
            else
            {
                status += "Robot Status: Idle";

                if (driveTimer != null)
                {
                    driveTimer.Dispose();
                    driveTimer = null;
                }
            }
        }

        private void Drive(object state)
        {
            byte leftLow = (byte)leftSpeed;
            byte leftHigh = (byte)(leftSpeed);
            byte rightLow = (byte)rightSpeed;
            byte rightHigh = (byte)(rightSpeed);
            host.Send(new byte[] { 0x01, 0x10, 0x11, leftHigh, leftLow, rightHigh, rightLow, 0xEF }, true);
        }

        public void PostMessage(string message)
        {

            Console.WriteLine(message);
        }

        public void HandleSystemMessage(byte[] buffer)
        {
            switch (buffer[1])
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

        public void beep(String input)
        {
            byte beep = Byte.Parse(input);
            host.Send(new byte[] { 0x01, 0x10, 0x00, beep, 0x00, 0x00, 0xEF }, true);
        }

        public void move(String input)
        {
            string[] speeds = input.Split(splitchar);
            try
            {
                leftSpeed = convert(int.Parse(speeds[0]));
                rightSpeed = convert(int.Parse(speeds[1]));
            }
            catch (IndexOutOfRangeException) { Console.WriteLine("Incorrect Input"); };
            System.Threading.Thread.Sleep(500);
            stop();
        }

        public void stop()
        {
            rightSpeed = 0;
            leftSpeed = 0;
        }

        public void HandleDataMessage(byte[] buffer)
        {
        }

        public string getLocation() {
            return "not implemented";
        }

        public string getFacing()
        {
            return "not implemented";
        }

        public byte convert(int speed) 
        {
            int max = 36;
            int min = -36;
            if (speed > max)
                return (byte)max;
            if (speed < 0)
            {
                if (speed < min)
                    return (byte)(255 + min);
                return (byte)(255 + speed);
            }
            return (byte)speed;
        }
    }
}
