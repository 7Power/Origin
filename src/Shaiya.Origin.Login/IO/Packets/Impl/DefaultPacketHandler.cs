using Shaiya.Origin.Common.Networking.Server.Session;
using System;

namespace Shaiya.Origin.Login.IO.Packets.Impl
{
    public class DefaultPacketHandler : PacketHandler
    {
        public override bool Handle(ServerSession session, int length, int opcode, byte[] data)
        {
            // Inform the user that an undefined packet is received
            Console.WriteLine("[Unhandled packet, Opcode: {0}, Length: {1}, Data: ", opcode, length);

            // Convert the byte array to a string
            string dataString = BitConverter.ToString(data);

            // Write the packet data, in hex form
            Console.WriteLine(String.Format("{0,10:X}]", dataString));

            return true;
        }
    }
}