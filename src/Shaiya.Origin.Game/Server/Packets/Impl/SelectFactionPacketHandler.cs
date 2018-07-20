using System;
using System.Collections.Generic;
using System.Text;
using Shaiya.Origin.Common.Logging;
using Shaiya.Origin.Common.Networking.Packets;
using Shaiya.Origin.Common.Networking.Server.Session;

namespace Shaiya.Origin.Game.Server.Packets.Impl
{
    public class SelectFactionPacketHandler : PacketHandler
    {

        /// <summary>
        /// Handles an incoming faction selection request.
        /// </summary>
        /// <param name="session">The session instance</param>
        /// <param name="length">The length of the packet</param>
        /// <param name="opcode">The opcode of the incoming packet</param>
        /// <param name="data">The packet data</param>
        /// <returns></returns>
        public override bool Handle(ServerSession session, int length, int opcode, byte[] data)
        {
            byte faction = (byte)(data[0] & 0xFF);

            // Verify that the faction is valid
            if (faction != Common.Game.Constants.FACTION_AOL && faction != Common.Game.Constants.FACTION_UOF)
            {
                Logger.Error("Player ID: {0} attempts to select an invalid faction with the value of {1}", session.GetGameIndex(), (int)faction);

                // Don't continue processing packets
                return false;
            }

            var dbClient = GameService.GetDbClient();

            var player = GameService.GetPlayerForIndex(session.GetGameIndex());

            var bldr = new PacketBuilder(Common.Database.Opcodes.SELECT_FACTION);

            bldr.WriteInt(player.index);

            bldr.WriteByte(faction);

            bldr.WriteByte((byte)GameService.GetServerId());

            dbClient.Write(bldr.ToPacket());

            player.faction = faction;

            return true;
        }
    }
}
