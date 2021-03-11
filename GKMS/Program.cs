using GKMS.Common;
using GKMS.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace GKMS
{
    class Program
    {
        private static Listener Listener;
        private static DatabaseContext Database;

        static void Main(string[] args)
        {
            var nics = NetworkInterface.GetAllNetworkInterfaces();

            Listener = new Listener();
            Database = new DatabaseContext();

            AppDomain.CurrentDomain.ProcessExit += (s, ev) =>
            {
                Listener.Dispose();
                Database.Dispose();
            };

            Listener.OnPacketReceived = PacketReceived;

            while (true) { }
        }

        static void PacketReceived(Packet packet, IPEndPoint ipe)
        {
            Packet response;

            switch (packet.Type)
            {
                case PacketType.LocateServer:
                    response = new Packet()
                    {
                        Type = PacketType.ServerLocated,
                        PhysicalAddress = packet.PhysicalAddress,
                        Message = "You found Waldo!"
                    };

                    Listener.Send(response, ipe);
                    break;

                case PacketType.ServerAllocateKey:
                    string gameType = packet.Message.Trim();
                    string key = GetKey(gameType, "");

                    response = new Packet()
                    {
                        Type = PacketType.ClientChangeKey,
                        PhysicalAddress = packet.PhysicalAddress,
                        Message = $"{gameType}|{key}"
                    };

                    Listener.Send(response, ipe);
                    break;
            }
        }

        static string GetKey(string gameType, string physicalAddress)
        {
            var game = GetGame(gameType);

            if (game != null)
            {
                var allocatedKeys = Database.KeyAllocations.Where(ka => ka.GameType == gameType).ToList();
                var currentKey = allocatedKeys.FirstOrDefault(ka => ka.PhysicalAddress == physicalAddress);

                if (currentKey != null)
                {
                    // Free up the currently allocated key
                    Database.KeyAllocations.Remove(currentKey);
                }

                var unallocatedKeys = game.Keys.Where(k => !allocatedKeys.Any(ka => ka.Key == k)).ToList();

                var newAllocation = new KeyAllocation()
                {
                    Key = unallocatedKeys[new Random().Next(0, unallocatedKeys.Count - 1)],
                    GameType = gameType,
                    PhysicalAddress = physicalAddress
                };

                Database.KeyAllocations.Add(newAllocation);

                Database.SaveChanges();

                return newAllocation.Key;
            }
            else
            {
                throw new Exception("Game not supported or JSON file not found.");
            }
        }

        static IGame GetGame(string gameType)
        {
            var type = Helpers.GetSupportedGameTypes().FirstOrDefault(gt => gt.Name == gameType);

            return GetGame(type);
        }

        static IGame GetGame(Type gameType)
        {
            Directory.CreateDirectory("Games");

            var path = Path.Combine("Games", $"{gameType.Name}.json");

            if (File.Exists(path))
            {
                var contents = File.ReadAllText(path);

                return (IGame)JsonConvert.DeserializeObject(contents, gameType);
            }
            else
            {
                var instance = (IGame)Activator.CreateInstance(gameType);

                instance.Keys = new string[] { };

                var contents = JsonConvert.SerializeObject(instance, Formatting.Indented);

                File.WriteAllText(path, contents);

                return instance;
            }
        }
    }
}
