using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace GKMS
{
    class Program
    {
        private static int HeartbeatPort = 420;
        static void Main(string[] args)
        {
            var blah = NetworkInterface.GetAllNetworkInterfaces();



            var heartbeat = new HeartbeatServer();

            heartbeat.Start();

            while (true) { }
        }
    }
}
