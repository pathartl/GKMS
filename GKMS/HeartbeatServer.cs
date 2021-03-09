using GKMS.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace GKMS
{
    public class HeartbeatServer
    {
        const int Port = 420;
        UdpClient Udp;
        GameService GameService { get; set; }

        public void Start()
        {
            GameService = new GameService();
            Udp = new UdpClient(Port);
            Udp.EnableBroadcast = true;
            Udp.BeginReceive(Receive, null);
        }

        void Send(Packet packet, IPEndPoint ipe)
        {
            UdpClient client = new UdpClient(0);

            byte[] packetBytes = packet.Encode();

            client.Send(packetBytes, packetBytes.Length, ipe);

            client.Close();
        }

        void Receive(IAsyncResult ar)
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Any, 0);

            byte[] data = Udp.EndReceive(ar, ref ipe);

            var request = new Packet(data);

            Packet response;

            switch (request.Type)
            {
                case PacketType.LocateServer:
                    response = new Packet()
                    {
                        Type = PacketType.ServerLocated,
                        PhysicalAddress = request.PhysicalAddress,
                        Message = "You found Waldo!"
                    };

                    Send(response, ipe);
                    break;

                case PacketType.ServerAllocateKey:
                    string gameType = request.Message.Trim();

                    response = new Packet()
                    {
                        Type = PacketType.ClientChangeKey,
                        PhysicalAddress = request.PhysicalAddress,
                        Message = $"{gameType}|{GameService.GetKey(gameType)}"
                    };

                    Send(response, ipe);
                    break;
            }

            Udp.BeginReceive(Receive, null);
        }
    }
}
