using System.Collections.Generic;

namespace Shaiya.Origin.Common.Networking.Packets
{
    public struct Packet
    {
        // The packet length
        public int length;

        // The packet opcode
        public int opcode;

        // The paylod of the packet
        public List<byte> payload;
    }
}