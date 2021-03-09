using GKMS.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GKMS
{
    public class GameService
    {
        public IEnumerable<IGame> Get()
        {
            List<IGame> games = new List<IGame>();

            var supportedGameTypes = GetSupportedGameTypes();

            foreach (var gameType in supportedGameTypes)
            {
                var path = Path.Combine("Games", $"{gameType.Name}.json");

                if (File.Exists(path))
                {
                    var contents = File.ReadAllText(path);

                    games.Add((IGame)JsonConvert.DeserializeObject(contents, gameType));
                }
            }

            return games;
        }

        public string GetKey(string gameType)
        {
            var game = GetGame(gameType);

            if (game != null)
            {
                return game.Keys[new Random().Next(0, game.Keys.Length - 1)];
            }
            else
            {
                throw new Exception("Game not supported or JSON file not found.");
            }
        }

        public IGame GetGame(string gameType)
        {
            var type = GetSupportedGameTypes().FirstOrDefault(gt => gt.Name == gameType);

            return GetGame(type);
        }

        public IGame GetGame(Type gameType)
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

        private IEnumerable<Type> GetSupportedGameTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IGame).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).ToList();
        }
    }
}
