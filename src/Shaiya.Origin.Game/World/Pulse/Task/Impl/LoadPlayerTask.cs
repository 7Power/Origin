using Shaiya.Origin.Common.Database.Structs.Game;
using Shaiya.Origin.Common.Networking.Client;
using Shaiya.Origin.Common.Networking.Packets;
using Shaiya.Origin.Common.Serializer;
using Shaiya.Origin.Game.Model.Entity.Player;
using System.Net;

namespace Shaiya.Origin.Game.World.Pulse.Task.Impl
{
    /// <summary>
    /// Represents a task used to load a player's details from the database server
    /// </summary>
    public class LoadPlayerTask : Task
    {
        private Player _player;

        /// <summary>
        /// The constructor for this task, which will define the player instance
        /// to operate on.
        /// </summary>
        /// <param name="player">The player instance</param>
        public LoadPlayerTask(Player player)
        {
            _player = player;
        }

        public override void Execute()
        {
            var dbClient = GameService.GetDbClient();

            var bldr = new PacketBuilder(Common.Database.Opcodes.GAME_USER_LOAD);

            bldr.WriteInt(_player.index);

            bldr.WriteByte((byte)GameService.GetServerId());

            var localPlayer = _player;

            dbClient.Write(bldr.ToPacket(), (_data, _length) =>
            {
                GameLoadPlayerResponse response = new GameLoadPlayerResponse();

                response = Serializer.Deserialize<GameLoadPlayerResponse>(_data);

                localPlayer.faction = response.faction;
                localPlayer.maxGameMode = response.maxGameMode;
                localPlayer.privilegeLevel = response.privilegeLevel;
                localPlayer.points = response.points;

                GameService.PushTask(new SendPlayerFactionTask(localPlayer));
                GameService.PushTask(new SendPlayerCharacterListTask(localPlayer));
            });
        }
    }
}