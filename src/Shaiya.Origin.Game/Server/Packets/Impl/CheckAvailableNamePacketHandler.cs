using System;
using System.Collections.Generic;
using System.Text;
using Shaiya.Origin.Common.Networking.Server.Session;
using Shaiya.Origin.Game.World.Pulse.Task.Impl;

namespace Shaiya.Origin.Game.Server.Packets.Impl
{
    public class CheckAvailableNamePacketHandler : PacketHandler
    {
        /// <summary>
        /// Handles an incoming request to check an available character name.
        /// </summary>
        /// <param name="session">The session instance</param>
        /// <param name="length">The length of the packet</param>
        /// <param name="opcode">The opcode of the incoming packet</param>
        /// <param name="data">The packet data</param>
        /// <returns></returns>
        public override bool Handle(ServerSession session, int length, int opcode, byte[] data)
        {
            byte[] characterName = new byte[length - 1];

            Array.Copy(data, 0, characterName, 0, length - 1);

            string name = Encoding.UTF8.GetString(characterName);

            var player = GameService.GetPlayerForIndex(session.GetGameIndex());

            GameService.PushTask(new CheckAvailableNameTask(player, name));

            return true;
        }
    }
}
