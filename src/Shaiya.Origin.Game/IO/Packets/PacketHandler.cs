using Shaiya.Origin.Common.Networking.Server.Session;

namespace Shaiya.Origin.Game.IO.Packets
{
    /// <summary>
    /// An abstract interface, used for handling incoming packets
    /// </summary>
    public abstract class PacketHandler
    {
        public abstract bool Handle(ServerSession session, int length, int opcode, byte[] data);
    }
}