using System;
using System.Collections.Generic;
using System.Text;

namespace RobotCommands
{
	public class CLocalBrainMessage
	{
		//misc
		public const byte EOT = 0xEF;

		//local message categories
		public const byte SYSTEM = 0x00;
		public const byte DATA = 0x01;

		//system messages
		public const byte PING = 0x00;
		public const byte PONG = 0x01;
		public const byte DISCONNECT = 0x02;
	}

	public class CRemoteBrainMessage
	{
		//misc
		public const byte EOT = 0xEF;

		//remote message categories
		public const byte SYSTEM = 0x00;
		public const byte MOTOR = 0x01;
		public const byte SERVO = 0x02;

		//system messages
		public const byte PING = 0x00;
		public const byte PONG = 0x01;
		public const byte DISCONNECT = 0x02;
	}
}
