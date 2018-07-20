using Shaiya.Origin.Common.Networking.Client;
using Shaiya.Origin.Common.Networking.Packets;
using Shaiya.Origin.Game.Model.Entity.Player;

namespace Shaiya.Origin.Game.World.Pulse.Task.Impl
{
    /// <summary>
    /// Represents a task used to check an available name from the database server.
    /// </summary>
    public class DeleteCharacterTask : Task
    {
        private Player _player;
        private int _characterId;

        /// <summary>
        /// The constructor for this task, which will define the player instance
        /// to operate on.
        /// </summary>
        /// <param name="player">The player instance.</param>
        /// <param name="name">The name to check.</param>
        public DeleteCharacterTask(Player player, int characterId)
        {
            _player = player;
            _characterId = characterId;
        }

        public override void Execute()
        {
            Player localPlayer = _player;

            OriginClient dbClient = GameService.GetDbClient();

            PacketBuilder bldr = new PacketBuilder(Common.Database.Opcodes.DELETE_CHARACTER);

            bldr.WriteInt(_player.index);

            bldr.WriteInt(_characterId);

            bldr.WriteByte((byte)GameService.GetServerId());

            dbClient.Write(bldr.ToPacket(), (_data, _length) =>
            {
                byte response = (byte)(_data[0] & 0xFF);

                PacketBuilder responseBldr = new PacketBuilder(Common.Packets.Opcodes.DELETE_CHARACTER);

                responseBldr.WriteByte(response);

                responseBldr.WriteInt(_characterId);

                localPlayer.Write(responseBldr.ToPacket());
            });
        }
    }
}