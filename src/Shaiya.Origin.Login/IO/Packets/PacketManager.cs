using Shaiya.Origin.Common.Packets;
using Shaiya.Origin.Login.IO.Packets.Impl;
using System.Collections.Generic;

namespace Shaiya.Origin.Login.IO.Packets
{
    public class PacketManager
    {
        private Dictionary<int, PacketHandler> _handlers = new Dictionary<int, PacketHandler>();
        private PacketHandler _handler;

        /// <summary>
        /// The <see cref="PacketManager"/> constructor, which is used to pre-define the packet handlers
        /// for the opcodes used by the Shaiya Origin login server.
        /// </summary>
        public PacketManager()
        {
            // The default packet handler
            _handlers[0] = new DefaultPacketHandler();

            // Define the packet handlers
            _handlers[Opcodes.LOGIN_TERMINATE] = new ConnectionTerminatedPacketHandler();
            _handlers[Opcodes.LOGIN_HANDSHAKE] = new HandshakePacketHandler();
            _handlers[Opcodes.LOGIN_REQUEST] = new LoginRequestPacketHandler();
            _handlers[Opcodes.SELECT_GAME_SERVER] = new ServerSelectPacketHandler();
        }

        /// <summary>
        /// Gets the handler assigned to a specific opcode
        /// </summary>
        /// <param name="opcode">The opcode</param>
        /// <returns>The handler if found. If not, the <see cref="DefaultPacketHandler"/> instance is returned</returns>
        public PacketHandler GetHandler(int opcode)
        {
            if (_handlers.TryGetValue(opcode, out _handler))
            {
                return _handler;
            }

            _handler = new DefaultPacketHandler();

            return _handler;
        }
    }
}