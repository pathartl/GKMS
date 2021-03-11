using GKMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GKMS.Common
{
    public class Listener : IDisposable
    {
        public const int Port = 420;
        UdpClient Udp;

        public _OnPacketReceived OnPacketReceived;

        public Listener()
        {
            Start();
        }

        public void Start()
        {
            Udp = new UdpClient(Port);
            Udp.EnableBroadcast = true;
            Udp.BeginReceive(Receive, null);
        }

        public void Send(Packet packet, IPEndPoint ipe)
        {
            var bytes = packet.Encode();

            Udp.SendAsync(bytes, bytes.Length, ipe);
        }

        void Receive(IAsyncResult ar)
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Any, 0);

            byte[] data = Udp.EndReceive(ar, ref ipe);

            var response = new Packet(data);

            

            OnPacketReceived(response, ipe);

            Udp.BeginReceive(Receive, null);
        }

        public delegate void _OnPacketReceived(Packet packet, IPEndPoint ipe);

        public void Dispose()
        {
            Udp.Close();
        }
    }
}
