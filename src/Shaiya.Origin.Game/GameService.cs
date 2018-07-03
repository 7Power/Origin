using Newtonsoft.Json.Linq;
using Shaiya.Origin.Common.Logging;
using Shaiya.Origin.Common.Networking.Client;
using Shaiya.Origin.Common.Service;
using Shaiya.Origin.Game.Server;
using System.Net;

namespace Shaiya.Origin.Game
{
    /// <summary>
    /// The game service, which is used to handle to game world, networking, and
    /// scripting environment.
    /// </summary>
    public class GameService : Service
    {
        private OriginClient _dbClient;
        private int _serverId;

        public override void Start()
        {
            Logger.Info("Initialising game world... ");

            _serverId = GetValueOrDefault("GameServerId", 1).Value<int>();

            var dbServerAddress = GetValueOrDefault("DatabaseServerAddress", "127.0.0.1");
            var dbServerPort = GetValueOrDefault("DatabaseServerPort", 30820);

            // The client instance
            _dbClient = new OriginClient(dbServerPort.Value<int>());

            // Connect to the db server
            if (!_dbClient.Connect(IPAddress.Parse(dbServerAddress.Value<string>()), dbServerPort.Value<int>()))
            {
                Logger.Error("Failed to connect to database server!");
                return;
            }

            Logger.Info("Successfully connected to the database server!");

            var socketServer = new SocketServer();

            var serverPort = GetValueOrDefault("GameServerPort", 30810);

            socketServer.Initialise(serverPort.Value<int>());
        }
    }
}