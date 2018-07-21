using Shaiya.Origin.Common.Networking.Packets;
using Shaiya.Origin.Common.Networking.Server.Session;
using System.Net;

namespace Shaiya.Origin.Login.IO.Packets.Impl
{
    public class ServerSelectPacketHandler : PacketHandler
    {
        private readonly object _syncObject = new object();

        /// <summary>
        /// Handles an incoming game server selection
        /// </summary>
        /// <param name="session">The session instance</param>
        /// <param name="length">The length of the packet</param>
        /// <param name="opcode">The opcode of the incoming packet</param>
        /// <param name="data">The packet data</param>
        /// <returns></returns>
        public override bool Handle(ServerSession session, int length, int opcode, byte[] data)
        {
            // If the length is not 5
            if (length != 5)
            {
                return true;
            }

            var bldr = new PacketBuilder(opcode);

            int serverId = (data[0] & 0xFF);

            int clientVersion = ((data[1] & 0xFF) + ((data[2] & 0xFF) << 8) + ((data[3] & 0xFF) << 16) + ((data[4] & 0xFF) << 24));

            lock (_syncObject)
            {
                var servers = LoginService.GetServers();

                Common.Database.Structs.Game.Server foundServer = new Common.Database.Structs.Game.Server();

                // Loop through the servers
                foreach (var server in servers)
                {
                    // If the server id matches
                    if (server.serverId == serverId)
                    {
                        foundServer = server;
                        break;
                    }
                }

                bldr.WriteByte(clientVersion == foundServer.clientVersion && foundServer.status == 0 ? (byte)serverId : unchecked((byte)-2));

                if (clientVersion == foundServer.clientVersion)
                {
                    IPAddress ipAddress = IPAddress.Parse(foundServer.ipAddress);

                    foreach (var i in ipAddress.GetAddressBytes())
                    {
                        bldr.WriteByte(i);
                    }
                }
            }

            session.Write(bldr.ToPacket());

            return true;
        }
    }
}