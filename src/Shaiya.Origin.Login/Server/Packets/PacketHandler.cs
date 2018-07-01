using Shaiya.Origin.Common.Networking.Server.Session;

namespace Shaiya.Origin.Login.Server.Packets
{
    public abstract class PacketHandler
    {
        public abstract bool Handle(ServerSession session, int length, int opcode, byte[] data);
    }
}