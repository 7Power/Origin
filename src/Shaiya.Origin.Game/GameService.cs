using Newtonsoft.Json.Linq;
using Shaiya.Origin.Common.Logging;
using Shaiya.Origin.Common.Networking.Client;
using Shaiya.Origin.Common.Service;
using Shaiya.Origin.Game.IO;
using Shaiya.Origin.Game.World.Pulse;
using Shaiya.Origin.Game.World.Pulse.Task;
using System.Net;
using System.Collections.Generic;
using Shaiya.Origin.Game.Model.Entity.Player;

namespace Shaiya.Origin.Game
{
    /// <summary>
    /// The game service, which is used to handle the game world, networking, and
    /// scripting environment.
    /// </summary>
    public class GameService : Service
    {
        private static OriginClient _dbClient;
        private static GamePulseHandler _pulseHandler;
        private static int _serverId;
        private static Dictionary<int, Player> _players = new Dictionary<int, Player>();

        public override void Start()
        {
            Logger.Info("Initialising game world... ");

            _serverId = GetValueOrDefault("GameServerId", 1).Value<int>();

            var dbServerAddress = GetValueOrDefault("DatabaseServerAddress", "127.0.0.1");
            var dbServerPort = GetValueOrDefault("DatabaseServerPort", 30820);

            // The client instance
            _dbClient = new OriginClient(IPAddress.Parse(dbServerAddress.Value<string>()), dbServerPort.Value<int>());

            // Connect to the db server
            if (!_dbClient.Connect())
            {
                Logger.Error("Failed to connect to database server!");
                return;
            }

            Logger.Info("Successfully connected to the database server!");

            _pulseHandler = new GamePulseHandler();

            _pulseHandler.Start();

            var socketServer = new SocketServer();

            var serverPort = GetValueOrDefault("GameServerPort", 30810);

            socketServer.Initialise(serverPort.Value<int>());
        }

        /// <summary>
        /// Attempts to load the player into the game world.
        /// </summary>
        /// <param name="player">The player instance</param>
        public static void LoadPlayer(Player player)
        {
            // Add the player instance
            _players.Add(player.index, player);

            _pulseHandler.Offer(new World.Pulse.Task.Impl.LoadPlayerTask(player));
        }

        /// <summary>
        /// Gets the player instance for an index
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>The player instance</returns>
        public static Player GetPlayerForIndex(int index)
        {
            return _players[index];
        }
        public static int GetServerId()
        {
            return _serverId;
        }

        public static void PushTask(Task task)
        {
            _pulseHandler.Offer(task);
        }

        public static OriginClient GetDbClient()
        {
            return _dbClient;
        }
    }
}