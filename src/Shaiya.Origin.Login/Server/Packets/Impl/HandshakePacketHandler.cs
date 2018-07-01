using Shaiya.Origin.Common.Networking.Server.Session;

namespace Shaiya.Origin.Login.Server.Packets.Impl
{
    public class HandshakePacketHandler : PacketHandler
    {
        public override bool Handle(ServerSession session, int length, int opcode, byte[] data)
        {
            // If the length is not 129
            if (length != 129)
            {
                return true;
            }

            // NOTE: This is used for setting up the encryption
            // The packet should be decrypted using the private RSA key, and then
            // we can take the AES Key and IV from the packet, and use it for decrypting incoming packets
            //
            // TODO: Finish this
            return true;
        }
    }
}