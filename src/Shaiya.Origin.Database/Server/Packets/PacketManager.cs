using Shaiya.Origin.Common.Database;
using Shaiya.Origin.Database.Server.Packets.Impl;
using System.Collections.Generic;

namespace Shaiya.Origin.Database.Server.Packets
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
            _handlers[Opcodes.USER_AUTH_REQUEST] = new UserAuthRequestHandler();
            _handlers[Opcodes.DELETE_SESSION] = new DeleteSessionRequestHandler();
            _handlers[Opcodes.SERVER_LIST] = new ServerListRequestHandler();
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