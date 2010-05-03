using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using RobotCommands;

namespace LocalBrainEmulator
{
    public class CRobotClient
    {
        private const int READ_BUFFER_SIZE = 512;

        private byte[] m_ReadBuffer;

        private Socket m_Socket;
        private IPAddress m_Address;
        private Stream m_NetworkStream;

        private AsyncCallback m_cbDataRead;
        private AsyncCallback m_cbDataSent;

        private MainForm parent;

        public bool IsConnected
        {
            get { return (m_Socket != null && m_Socket.Connected); }
        }

        public CRobotClient(MainForm p)
        {
            parent = p;

            m_Socket = null;
            m_NetworkStream = null;
            m_ReadBuffer = new byte[READ_BUFFER_SIZE];

            m_cbDataRead = new AsyncCallback(OnDataRead);
            m_cbDataSent = new AsyncCallback(OnDataSent);
        }

        public void Connect(IPAddress address)
        {
            m_Address = address;

            try
            {
                m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_Socket.Connect(new IPEndPoint(address, 48888));

                m_NetworkStream = new NetworkStream(m_Socket);
                m_NetworkStream.BeginRead(m_ReadBuffer, 0, READ_BUFFER_SIZE, m_cbDataRead, null);

                parent.PostMessage("Successfully connected to robot host.");
            }
            catch (Exception ex)
            {
                parent.PostMessage("Error connecting to robot host.\r\n  " + ex.Message);

                m_Socket = null;
                m_NetworkStream = null;
            }

            parent.UpdateStatus();
        }

        public void Disconnect(bool notify)
        {
            if (notify)
                Send(new byte[] { CLocalBrainMessage.SYSTEM, CLocalBrainMessage.DISCONNECT, CLocalBrainMessage.EOT }, false);

            m_Socket.Close();
            m_Socket = null;

            parent.PostMessage("Disconnected from robot host.");
            parent.UpdateStatus();
        }

        public void Send(byte[] buffer, bool async)
        {
            if (m_Socket == null || !m_Socket.Connected)
                return;

            try
            {
                if (async)
                    m_NetworkStream.BeginWrite(buffer, 0, buffer.Length, m_cbDataSent, null);
                else
                    m_NetworkStream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                parent.PostMessage("Error sending message to robot host.\r\n  " + ex.Message);
            }
        }

        public void OnDataRead(IAsyncResult ar)
        {
            if (m_Socket == null || !m_Socket.Connected)
                return;

            try
            {
                int bytesReceived = m_NetworkStream.EndRead(ar);

                if (bytesReceived > 0)
                {
                    byte[] buffer = new byte[bytesReceived];
                    for (int i = 0; i < buffer.Length; i++)
                        buffer[i] = m_ReadBuffer[i];

                    m_NetworkStream.BeginRead(m_ReadBuffer, 0, READ_BUFFER_SIZE, m_cbDataRead, null);

                    switch (buffer[0])
                    {
                        case CRemoteBrainMessage.SYSTEM:
                            parent.HandleSystemMessage(buffer);
                            break;

                        case CRemoteBrainMessage.SERVO:
                            parent.HandleServosMessage(buffer);
                            break;

                        case CRemoteBrainMessage.MOTOR:
                            parent.HandleMotorsMessage(buffer);
                            break;

                        default:
                            parent.PostMessage("Unknown message type received from robot host.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                parent.PostMessage("Error receiving message from robot host.\r\n  " + ex.Message);
            }
        }

        public void OnDataSent(IAsyncResult ar)
        {
            try
            {
                m_NetworkStream.EndWrite(ar);
            }
            catch (Exception ex)
            {
                parent.PostMessage("Error sending message to robot host.\r\n  " + ex.Message);
            }
        }
    }
}

