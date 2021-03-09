using GKMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GKMS.Agent
{
    public class Listener
    {
        const int Port = 420;
        UdpClient Udp;

        public void Start()
        {
            Udp = new UdpClient(Port);
            Udp.EnableBroadcast = true;
            Udp.BeginReceive(Receive, null);
        }

        void Send(Packet packet, IPEndPoint ipe)
        {

        }

        void Receive(IAsyncResult ar)
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Any, 0);

            byte[] data = Udp.EndReceive(ar, ref ipe);
            byte[] physicalAddress = new byte[6];

            var nics = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var nic in nics)
            {
                var ipProperties = nic.GetIPProperties();

                if (ipProperties.UnicastAddresses.Any(ua => ua.Address.AddressFamily == AddressFamily.InterNetwork && ua.Address.Equals(ipe.Address)))
                {
                    physicalAddress = nic.GetPhysicalAddress().GetAddressBytes();
                }
            }

            var request = new Packet(data);

            Packet response;

            switch (request.Type)
            {
                case PacketType.LocateClients:
                    response = new Packet()
                    {
                        Type = PacketType.ClientLocated,
                        PhysicalAddress = physicalAddress,
                    };

                    Send(response, ipe);
                    break;

                case PacketType.ClientChangeKey:
                    var messageParts = request.Message.Split('|');

                    var gameType = GetSupportedGameTypes().FirstOrDefault(gt => gt.Name == messageParts[0]);

                    if (gameType != null)
                    {
                        IGame game = (IGame)Activator.CreateInstance(gameType);

                        game.ChangeKey(messageParts[1]);
                    }
                    break;
            }
        }

        private IEnumerable<Type> GetSupportedGameTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IGame).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).ToList();
        }
    }
}
