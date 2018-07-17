using Shaiya.Origin.Common.Packets;
using Shaiya.Origin.Game.Server.Packets.Impl;
using System.Collections.Generic;

namespace Shaiya.Origin.Game.Server.Packets
{
    public class PacketManager
    {
        private Dictionary<int, PacketHandler> _handlers = new Dictionary<int, PacketHandler>();
        private PacketHandler _handler;

        public PacketManager()
        {
            // The default packet handler
            _handlers[0] = new DefaultPacketHandler();

            // Define the database server packet handlers
            _handlers[Opcodes.GAME_HANDSHAKE] = new GameHandshakePacketHandler();
            _handlers[Opcodes.ACCOUNT_FACTION] = new SelectFactionPacketHandler();
            _handlers[Opcodes.AVAILABLE_CHARACTER_NAME] = new CheckAvailableNamePacketHandler();
            _handlers[Opcodes.CREATE_CHARACTER] = new CreateCharacterPacketHandler();
            _handlers[Opcodes.DELETE_CHARACTER] = new DeleteCharacterPacketHandler();
            _handlers[Opcodes.CHARACTER_SELECTION] = new SelectCharacterPacketHandler();
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