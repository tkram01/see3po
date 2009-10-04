using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace LocalBrain
{
	class CServosController
	{
		private const byte EOT = 0x0d;

		private SerialPort m_SerialPort;
		private MainForm parent;

		public bool IsConnected
		{
			get { return (m_SerialPort != null && m_SerialPort.IsOpen); }
		}

		public CServosController(MainForm p)
		{
			parent = p;

			m_SerialPort = new SerialPort("COM2", 115200, Parity.None, 8, StopBits.One);
			m_SerialPort.Handshake = Handshake.None;
			m_SerialPort.DiscardNull = false;
			m_SerialPort.RtsEnable = true;
		}

		public void Connect()
		{
			m_SerialPort.Open();

			parent.UpdateStatus();
			parent.PostMessage("Successfully connected to servos.");
		}

		public void Disconnect()
		{
			m_SerialPort.Close();

			parent.UpdateStatus();
			parent.PostMessage("Disconnected from servos.");
		}

		public void CenterServo(int servo)
		{
			m_SerialPort.Write("#" + servo.ToString() + " P1500\r");
		}

		public void MoveServoTo(int servo, int pw)
		{
			m_SerialPort.Write("#" + servo.ToString() + " P" + pw.ToString() + "\r");
		}

		public void Send(byte[] buffer)
        {
            m_SerialPort.Write(buffer, 0, buffer.Length);
        }
	}
}
