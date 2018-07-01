using Shaiya.Origin.Common.Database.Structs.Auth;
using Shaiya.Origin.Common.Networking.Client;
using Shaiya.Origin.Common.Networking.Packets;
using Shaiya.Origin.Common.Networking.Server.Session;
using Shaiya.Origin.Common.Serializer;
using System;
using System.Linq;
using System.Net;
using System.Text;

namespace Shaiya.Origin.Login.Server.Packets.Impl
{
    public class LoginRequestPacketHandler : PacketHandler
    {
        private static readonly object _syncObject = new object();

        /// <summary>
        /// Handles an incoming login request
        /// </summary>
        /// <param name="session">The session instance</param>
        /// <param name="length">The lenght of the packet</param>
        /// <param name="opcode">The opcode of the incoming packet</param>
        /// <param name="data">The packet data</param>
        /// <returns></returns>
        public override bool Handle(ServerSession session, int length, int opcode, byte[] data)
        {
            // If the packet is not the correct length
            if (length != 48)
            {
                return true;
            }

            byte[] username = new byte[18];
            byte[] password = new byte[16];

            Array.Copy(data, 0, username, 0, 18);
            Array.Copy(data, 32, password, 0, 16);

            var dbClient = new OriginClient(30820);

            AuthRequest authRequest = new AuthRequest();

            byte[] ipAddress = new byte[15];
            ipAddress = Encoding.UTF8.GetBytes(session.GetRemoteAdress());

            authRequest.username = username;
            authRequest.password = password;
            authRequest.ipAddress = ipAddress;

            var bldr = new PacketBuilder(Common.Database.Opcodes.USER_AUTH_REQUEST);

            var array = Serializer.Serialize(authRequest);

            bldr.WriteBytes(array, array.Length);

            var current = this;

            IPAddress ipadress = IPAddress.Parse("127.0.0.1");

            dbClient.Connect(ipadress, 30820);

            dbClient.Write(bldr.ToPacket(), (_data, _length) =>
            {
                AuthResponse authResponse = new AuthResponse();

                authResponse = (AuthResponse)Serializer.Deserialize(_data, authResponse);

                var builder = new PacketBuilder(Common.Packets.Opcodes.LOGIN_REQUEST);

                int status = authResponse.status;

                builder.WriteInt(status);

                if (status == 0)
                {
                    builder.WriteInt(authResponse.userId);

                    builder.WriteInt(authResponse.privilegeLevel);

                    builder.WriteBytes(authResponse.identityKeys, 16);

                    session.SetIdentityKeys(authResponse.identityKeys);

                    current.HandleServerList(session);
                }

                session.Write(builder.ToPacket());
            });

            return true;
        }

        /// <summary>
        /// Handles a server list response, and sends the server list to the user
        /// </summary>
        /// <param name="session">The session instance</param>
        public void HandleServerList(ServerSession session)
        {
            var bldr = new PacketBuilder(Common.Packets.Opcodes.SERVER_LIST_DETAILS);

            lock (_syncObject)
            {
                var servers = LoginService.GetServers();

                byte serverCount = (byte)servers.Count;

                bldr.WriteByte(serverCount);

                // Loop through the servers
                for (int i = 0; i < serverCount; i++)
                {
                    var server = servers.ElementAt(i);

                    // TODO: Properly find the values that these fields should be
                    // or how they should be manipulated
                    bldr.WriteShort(server.serverId);
                    bldr.WriteShort(server.status);
                    bldr.WriteShort(server.population);
                    bldr.WriteBytes(Encoding.UTF8.GetBytes(server.serverName), Encoding.UTF8.GetByteCount(server.serverName));
                }
            }

            session.Write(bldr.ToPacket());
        }
    }
}