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
        private static DatabaseContext Context;

        static void Main(string[] args)
        {
            var nics = NetworkInterface.GetAllNetworkInterfaces();

            Listener = new Listener();
            Context = new DatabaseContext();

            Context.Database.EnsureCreated();

            AppDomain.CurrentDomain.ProcessExit += (s, ev) =>
            {
                Listener.Dispose();
                Context.Dispose();
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
                        Message = ipe.Address.ToString()
                    };

                    Listener.Send(response, ipe);
                    break;

                case PacketType.ServerAllocateKey:
                    string gameType = packet.Message.Trim();
                    string physicalAddress = new PhysicalAddress(packet.PhysicalAddress).ToString();
                    string key = GetKey(gameType, physicalAddress);

                    if (key != "")
                    {
                        response = new Packet()
                        {
                            Type = PacketType.ClientChangeKey,
                            PhysicalAddress = packet.PhysicalAddress,
                            Message = $"{gameType}|{key}"
                        };
                    }
                    else
                    {
                        response = new Packet()
                        {
                            Type = PacketType.NoAvailableKeys,
                            PhysicalAddress = packet.PhysicalAddress,
                            Message = $"No keys were available."
                        };
                    }

                    Listener.Send(response, ipe);
                    break;
            }
        }

        static string GetKey(string gameType, string physicalAddress)
        {
            var game = GetGame(gameType);

            if (game != null)
            {
                var allocatedKeys = Context.KeyAllocations.Where(ka => ka.GameType == gameType).ToList();
                var currentKey = allocatedKeys.FirstOrDefault(ka => ka.PhysicalAddress == physicalAddress);

                if (currentKey != null)
                {
                    // Free up the currently allocated key
                    Context.KeyAllocations.Remove(currentKey);
                }

                var unallocatedKeys = game.Keys.Where(k => !allocatedKeys.Any(ka => ka.Key == k)).ToList();

                if (game.Keys.Length != 0)
                {
                    var newAllocation = new KeyAllocation()
                    {
                        Key = unallocatedKeys[new Random().Next(0, unallocatedKeys.Count - 1)],
                        GameType = gameType,
                        PhysicalAddress = physicalAddress
                    };

                    Context.KeyAllocations.Add(newAllocation);

                    Context.SaveChanges();

                    return newAllocation.Key;
                }
                else
                {
                    return "";
                }
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
