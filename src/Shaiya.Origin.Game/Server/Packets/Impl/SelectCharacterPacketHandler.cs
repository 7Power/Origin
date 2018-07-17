using Shaiya.Origin.Common.Networking.Server.Session;
using Shaiya.Origin.Game.World.Pulse.Task.Impl;
using System;

namespace Shaiya.Origin.Game.Server.Packets.Impl
{
    public class SelectCharacterPacketHandler : PacketHandler
    {
        /// <summary>
        /// Handles an incoming request to enter the game world with a character.
        /// </summary>
        /// <param name="session">The session instance</param>
        /// <param name="length">The length of the packet</param>
        /// <param name="opcode">The opcode of the incoming packet</param>
        /// <param name="data">The packet data</param>
        /// <returns></returns>
        public override bool Handle(ServerSession session, int length, int opcode, byte[] data)
        {
            // If the length is not valid
            if (length != 4)
            {
                return true;
            }

            int characterId = BitConverter.ToInt32(data);

            var player = GameService.GetPlayerForIndex(session.GetGameIndex());

            GameService.PushTask(new LoadCharacterGameWorldTask(player, characterId));

            return true;
        }
    }
}