using Newtonsoft.Json.Linq;
using Shaiya.Origin.Common.Logging;
using Shaiya.Origin.Common.Service;
using Shaiya.Origin.Database.Connector;
using Shaiya.Origin.Database.Server;

namespace Shaiya.Origin.Database
{
    public class DatabaseService : Service
    {
        public override void Start()
        {
            var connector = new DatabaseConnector();

            if (!connector.Connect("postgres"))
            {
                Logger.Error("Failed to connect to database!");
                return;
            }

            Logger.Info("Successfully connected to the PostgreSQL database!");

            var socketServer = new SocketServer();

            var serverPort = GetValueOrDefault("DatabaseServerPort", 30820);

            socketServer.Initialise(serverPort.Value<int>());
        }
    }
}