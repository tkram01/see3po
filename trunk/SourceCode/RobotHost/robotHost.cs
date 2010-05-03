using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

using RobotCommands;

namespace RobotHost
{
	public interface IRobotParent
	{
		void UpdateStatus();
		void PostMessage(string msg);
		void HandleSystemMessage(byte[] buffer);
		void HandleDataMessage(byte[] buffer);
	}

	public class CRobotHost
	{
		public delegate void DPostMessage();
		public delegate void DHandleMessage(byte[] buffer);

		private const int READ_BUFFER_SIZE = 512;

		private Socket m_ListenSocket;
		private Socket m_ClientSocket;
		private byte[] m_ReadBuffer;

		private AsyncCallback m_cbDataRead;
		private AsyncCallback m_cbDataSent;
		private AsyncCallback m_cbAccepted;

		IRobotParent m_Parent;

		public bool IsListening
		{
			get { return m_ListenSocket != null; }
		}
		public bool IsConnected
		{
			get { return (m_ClientSocket != null && m_ClientSocket.Connected); }
		}

		public CRobotHost(IRobotParent parent)
		{
			m_Parent = parent;

			m_ReadBuffer = new byte[READ_BUFFER_SIZE];
			m_ListenSocket = null;
			m_ClientSocket = null;

			m_cbDataRead = new AsyncCallback(OnDataRead);
			m_cbDataSent = new AsyncCallback(OnDataSent);
			m_cbAccepted = new AsyncCallback(OnAccept);
		}

		public void StartListening()
		{
			try
			{
				m_ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				IPEndPoint local = new IPEndPoint(IPAddress.Any, 48888);
				m_ListenSocket.Bind(local);
				m_ListenSocket.Listen(1);

				m_ListenSocket.BeginAccept(new AsyncCallback(OnAccept), null);
			}
			catch(Exception ex)
			{
				m_Parent.PostMessage("Error listening for new connections.\r\n  " + ex.Message);
			}

			m_Parent.UpdateStatus();
			m_Parent.PostMessage("Now listening for new connections.");
		}

		public void StopListening()
		{
			if(m_ListenSocket == null)
				return;

			m_ListenSocket.Close();
			m_ListenSocket = null;

			m_Parent.UpdateStatus();
			m_Parent.PostMessage("Stopped listening for new connections.");
		}

		public void Disconnect(bool notify)
		{
			if(notify)
				Send(new byte[] {CRemoteBrainMessage.SYSTEM, CRemoteBrainMessage.DISCONNECT, CRemoteBrainMessage.EOT}, false);

			m_ClientSocket.Disconnect(true);
			m_ClientSocket.Close();
			m_ClientSocket = null;

			m_Parent.UpdateStatus();
			m_Parent.PostMessage("Disconnected from client.");
		}

		public void Send(byte[] buffer, bool async)
		{
			if(m_ClientSocket == null || !m_ClientSocket.Connected)
				return;

			try
			{
				if(async)
					m_ClientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, m_cbDataSent, null);
				else
					m_ClientSocket.Send(buffer);
			}
			catch(Exception ex)
			{
				m_Parent.PostMessage("Error sending message to robot client.\r\n  " + ex.Message);
			}
		}

		public void Send(string str, bool async)
		{
			if(m_ClientSocket == null || !m_ClientSocket.Connected)
				return;

			byte[] buffer = new byte[str.Length];
			for(int i = 0; i < str.Length; i++)
				buffer[i] = (byte)str[i];

			try
			{
				if(async)
					m_ClientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, m_cbDataSent, null);
				else
					m_ClientSocket.Send(buffer);
			}
			catch(Exception ex)
			{
				m_Parent.PostMessage("Error sending message to robot client.\r\n  " + ex.Message);
			}
		}

		public void Close()
		{
			if(m_ClientSocket != null)
			{
				m_ClientSocket.Close();
				m_ClientSocket = null;
			}

			if(m_ListenSocket != null)
			{
				m_ListenSocket.Close();
				m_ListenSocket = null;
			}
		}

		private void OnAccept(IAsyncResult ar)
		{
			if(m_ListenSocket == null)
				return;

			try
			{
				m_ClientSocket = m_ListenSocket.EndAccept(ar);
				m_ClientSocket.BeginReceive(m_ReadBuffer, 0, READ_BUFFER_SIZE, SocketFlags.None, m_cbDataRead, null);

				m_ListenSocket.Close();
				m_ListenSocket = null;
			}
			catch(Exception ex)
			{
				m_Parent.PostMessage("Error accepting new connection.\r\n  " + ex.Message);
			}

			m_Parent.UpdateStatus();
			m_Parent.PostMessage("Successfully connected to robot client.");
		}

		private void OnDataRead(IAsyncResult ar)
		{
			if(m_ClientSocket == null || !m_ClientSocket.Connected)
				return;

			try
			{
				int bytesRead = m_ClientSocket.EndReceive(ar);

				if(bytesRead > 0)
				{
					byte[] buffer = new byte[m_ReadBuffer.Length];
					for(int i = 0; i < buffer.Length; i++)
						buffer[i] = m_ReadBuffer[i];

					m_ClientSocket.BeginReceive(m_ReadBuffer, 0, READ_BUFFER_SIZE, SocketFlags.None, m_cbDataRead, null);

					switch(buffer[0])
					{
						case CLocalBrainMessage.SYSTEM:
							m_Parent.HandleSystemMessage(buffer);
							break;

						case CLocalBrainMessage.DATA:
							m_Parent.HandleDataMessage(buffer);
							break;



						default:
							m_Parent.PostMessage("Unknown message type received from robot client.");
							break;
					}
				}
			}
			catch(Exception ex)
			{
				m_Parent.PostMessage("Error receiving message from robot client.\r\n  " + ex.Message);
			}
		}

		private void OnDataSent(IAsyncResult ar)
		{
			try
			{
				m_ClientSocket.EndSend(ar);
			}
			catch(Exception ex)
			{
				m_Parent.PostMessage("Error sending message to robot client.\r\n  " + ex.Message);
			}
		}
	}
}
