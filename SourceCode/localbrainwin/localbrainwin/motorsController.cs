using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace LocalBrain
{
	class CMotorsController
	{
        private const byte EOT = 0xEF;

        private SerialPort m_SerialPort;
		private MainForm parent;

        public bool IsConnected
        {
            get { return (m_SerialPort != null && m_SerialPort.IsOpen); }
        }

        public CMotorsController(MainForm p)
        {
			parent = p;

			m_SerialPort = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
			m_SerialPort.Handshake = Handshake.None;
			m_SerialPort.DiscardNull = false;
			m_SerialPort.RtsEnable = true;
        }

        public void Connect()
        {
            try
            {
                m_SerialPort.Open();
                parent.PostMessage("Successfully connected to motors.");
            }
            catch (Exception ex)
            {
                parent.PostMessage("COM1 open error:"+ ex.ToString());
            }

			parent.UpdateStatus();
			
        }

        public void Disconnect()
        {
            m_SerialPort.Close();

			parent.UpdateStatus();
			parent.PostMessage("Disconnected from motors.");
        }

        public void Send(byte[] buffer)
        {
			if(m_SerialPort == null || !m_SerialPort.IsOpen)
				return;

            m_SerialPort.Write(buffer, 0, buffer.Length);
        }
	}
}
