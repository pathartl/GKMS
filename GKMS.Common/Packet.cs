using System;
using System.Collections.Generic;
using System.Text;

namespace GKMS.Common
{
    public class Packet
    {
        public PacketType Type { get; set; }
        public byte[] PhysicalAddress = new byte[6];
        public string Message { get; set; }

        public Packet() { }

        public Packet(byte[] bytes)
        {
            Decode(bytes);
        }

        public byte[] Encode()
        {
            // Remaining bytes after header
            if (Message.Length > 1017)
            {
                throw new ArgumentOutOfRangeException("Message is too long.");
            }

            var packet = new byte[1024];

            packet[0] = Convert.ToByte((int)Type);

            for (int i = 0; i < 6; i++)
            {
                packet[i + 1] = PhysicalAddress[i];
            }

            var messageBytes = Encoding.ASCII.GetBytes(Message);

            for (int i = 0; i < Message.Length; i++)
            {
                packet[i + 7] = messageBytes[i];
            }

            return packet;
        }

        public void Decode(byte[] bytes)
        {
            Type = (PacketType)Convert.ToInt32(bytes[0]);

            for (int i = 0; i < 6; i++)
            {
                PhysicalAddress[i] = bytes[i + 1];
            }

            var messageBytes = new byte[bytes.Length - 7];

            for (int i = 7; i < bytes.Length; i++)
            {
                messageBytes[i - 7] = bytes[i];
            }
        }
    }
}
