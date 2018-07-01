using Shaiya.Origin.Common.Networking.Client;
using Shaiya.Origin.Common.Networking.Packets;
using Shaiya.Origin.Common.Networking.Server.Session;
using System.Net;

namespace Shaiya.Origin.Login.Server.Packets.Impl
{
    public class ConnectionTerminatedPacketHandler : PacketHandler
    {
        /// <summary>
        /// Handles a terminated connection packet
        /// </summary>
        /// <param name="session">The session instance</param>
        /// <param name="length">The length of the packet</param>
        /// <param name="opcode">The opcode of the incoming packet</param>
        /// <param name="data">The packet data</param>
        /// <returns></returns>
        public override bool Handle(ServerSession session, int length, int opcode, byte[] data)
        {
            var identityKeys = session.GetIdentityKeys();

            var dbClient = new OriginClient(30820);

            var bldr = new PacketBuilder(Common.Database.Opcodes.DELETE_SESSION);

            bldr.WriteBytes(identityKeys, 16);

            IPAddress ipadress = IPAddress.Parse("127.0.0.1");
            dbClient.Connect(ipadress, 30820);

            dbClient.Write(bldr.ToPacket());

            byte[] emptyKeys = new byte[16];

            session.SetIdentityKeys(emptyKeys);

            session.Close();

            return false;
        }
    }
}