using Shaiya.Origin.Common.Networking.Packets;
using Shaiya.Origin.Game.Model.Entity.Player;

namespace Shaiya.Origin.Game.World.Pulse.Task.Impl
{
    public class SendPlayerFactionTask : Task
    {
        private Player _player;

        /// <summary>
        /// The constructor for this task, which will define the player instance
        /// to operate on.
        /// </summary>
        /// <param name="player">The player instance</param>
        public SendPlayerFactionTask(Player player)
        {
            _player = player;
        }

        /// <summary>
        /// Handle the sending of a player's faction
        /// </summary>
        public override void Execute()
        {
            var bldr = new PacketBuilder(Common.Packets.Opcodes.ACCOUNT_FACTION);

            bldr.WriteByte((byte)_player.faction);
            bldr.WriteByte((byte)_player.maxGameMode);

            _player.Write(bldr.ToPacket());
        }
    }
}