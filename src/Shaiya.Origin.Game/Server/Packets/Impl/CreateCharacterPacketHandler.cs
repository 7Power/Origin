using Shaiya.Origin.Common.Database.Structs.Game;
using Shaiya.Origin.Common.Networking.Server.Session;
using Shaiya.Origin.Game.World.Pulse.Task.Impl;
using System;

namespace Shaiya.Origin.Game.Server.Packets.Impl
{
    public class CreateCharacterPacketHandler : PacketHandler
    {
        /// <summary>
        /// Handles an incoming character creation request.
        /// </summary>
        /// <param name="session">The session instance</param>
        /// <param name="length">The length of the packet</param>
        /// <param name="opcode">The opcode of the incoming packet</param>
        /// <param name="data">The packet data</param>
        /// <returns></returns>
        public override bool Handle(ServerSession session, int length, int opcode, byte[] data)
        {
            var request = new CreateCharacterRequest();

            request.unknown = data[0];
            request.race = data[1];
            request.mode = data[2];
            request.hair = data[3];
            request.face = data[4];
            request.height = data[5];
            request.profession = data[6];
            request.sex = data[7];
            Array.Copy(data, 8, request.name, 0, length - 8);

            var player = GameService.GetPlayerForIndex(session.GetGameIndex());

            GameService.PushTask(new CreateCharacterTask(player, request));

            return true;
        }
    }
}