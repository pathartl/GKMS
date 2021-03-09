using GKMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GKMS.Client
{
    public class Listener : IDisposable
    {
        const int Port = 420;
        UdpClient Udp;

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

            Udp.Send(bytes, bytes.Length, ipe);
        }

        void Receive(IAsyncResult ar)
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Any, 0);

            byte[] data = Udp.EndReceive(ar, ref ipe);
            byte[] physicalAddress = new byte[6];

            var request = new Packet(data);

            Packet response;

            switch (request.Type)
            {
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

        public void Dispose()
        {
            Udp.Close();
        }
    }
}
