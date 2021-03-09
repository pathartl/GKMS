using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace GKMS.Server
{
    public class HeartbeatServer
    {
        const int Port = 420;
        UdpClient udp;

        public void Start()
        {
            udp = new UdpClient(Port);
            udp.EnableBroadcast = true;
            udp.BeginReceive(Receive, null);
        }

        void Send(string msg, IPEndPoint ipe)
        {
            UdpClient client = new UdpClient(0);

            byte[] m = Encoding.ASCII.GetBytes(msg);

            client.Send(m, m.Length, ipe);

            client.Close();
        }

        void Receive(IAsyncResult ar)
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Any, 0);

            byte[] data = udp.EndReceive(ar, ref ipe);

            string msg = Encoding.ASCII.GetString(data);

            Send("PONG", ipe);

            udp.BeginReceive(Receive, null);
        }
    }
}
