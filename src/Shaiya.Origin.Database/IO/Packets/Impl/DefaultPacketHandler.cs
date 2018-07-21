using Shaiya.Origin.Common.Networking.Server.Session;
using System;

namespace Shaiya.Origin.Database.IO.Packets.Impl
{
    public class DefaultPacketHandler : PacketHandler
    {
        /// <summary>
        /// Handles an undefined packet
        /// </summary>
        /// <param name="session">The session instance</param>
        /// <param name="length">The length of the packet</param>
        /// <param name="opcode">The opcode of the incoming packet</param>
        /// <param name="data">The packet data</param>
        public override bool Handle(ServerSession session, int length, int opcode, int requestId, byte[] data)
        {
            // Inform the user that an undefined packet is received
            Console.WriteLine("[Unhandled packet, Opcode: {0}, Length: {1}, Data: ", opcode, length);

            // Convert the byte array to a string
            string dataString = BitConverter.ToString(data);

            // Write the packet data, in hex form
            Console.WriteLine(String.Format("{0,10:X}]", dataString));

            // Return true
            return true;
        }
    }
}