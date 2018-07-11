using Newtonsoft.Json.Linq;
using Shaiya.Origin.Common.Logging;
using Shaiya.Origin.Common.Networking.Client;
using Shaiya.Origin.Common.Networking.Packets;
using Shaiya.Origin.Common.Serializer;
using Shaiya.Origin.Common.Service;
using Shaiya.Origin.Login.Server;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace Shaiya.Origin.Login
{
    public class LoginService : Service
    {
        private static OriginClient _dbClient;
        private static List<Common.Database.Structs.Game.Server> _servers = new List<Common.Database.Structs.Game.Server>();

        private static readonly object _syncObject = new object();

        public override void Start()
        {
            var dbServerAddress = GetValueOrDefault("DatabaseServerAddress", "127.0.0.1");
            var dbServerPort = GetValueOrDefault("DatabaseServerPort", 30820);

            // The client instance
            _dbClient = new OriginClient(IPAddress.Parse(dbServerAddress.Value<string>()),dbServerPort.Value<int>());

            // Connect to the db server
            if (!_dbClient.Connect())
            {
                Logger.Error("Failed to connect to database server!");
                return;
            }

            Logger.Info("Successfully connected to the database server!");

            Thread listThread = new Thread(UpdateServerList);
            listThread.Start();

            var socketServer = new SocketServer();

            var serverPort = GetValueOrDefault("LoginServerPort", 30800);

            socketServer.Initialise(serverPort.Value<int>());
        }

        public void UpdateServerList()
        {
            // The server update delay
            var updateDelay = GetValueOrDefault("WorldStatsUpdateDelay", 30);

            // Loop and update the server list every 30s
            while (true)
            {
                var bldr = new PacketBuilder(Common.Database.Opcodes.SERVER_LIST);

                _dbClient.Write(bldr.ToPacket(), (_data, _length) =>
                {
                    int serverCount = _data[0] & 0xFF;

                    var data = new byte[_length - 4];
                    Array.Copy(_data, 4, data, 0, _length - 4);

                    lock (_syncObject)
                    {
                        _servers.Clear();

                        for (int i = 0; i < serverCount; i++)
                        {
                            Common.Database.Structs.Game.Server server = new Common.Database.Structs.Game.Server();

                            server = Serializer.Deserialize<Common.Database.Structs.Game.Server>(data);

                            _servers.Add(server);
                        }
                    }
                });

                Thread.Sleep(TimeSpan.FromSeconds(updateDelay.Value<int>()));
            }
        }

        public static List<Common.Database.Structs.Game.Server> GetServers()
        {
            return _servers;
        }

        public static OriginClient GetDbClient()
        {
            return _dbClient;
        }
    }
}