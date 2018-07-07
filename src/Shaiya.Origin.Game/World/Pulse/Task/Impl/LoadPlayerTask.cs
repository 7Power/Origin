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
            var dbClient = new OriginClient(30820);

            var bldr = new PacketBuilder(Common.Database.Opcodes.GAME_USER_LOAD);

            bldr.WriteInt(_player.GetIndex());

            bldr.WriteByte((byte)GameService.GetServerId());

            var localPlayer = _player;

            IPAddress ipadress = IPAddress.Parse("127.0.0.1");

            dbClient.Connect(ipadress, 30820);

            dbClient.Write(bldr.ToPacket(), (_data, _length) =>
            {
                GameLoadPlayerResponse response = new GameLoadPlayerResponse();

                response = (GameLoadPlayerResponse)Serializer.Deserialize(_data, response);

                localPlayer.SetFaction(response.faction);
                localPlayer.SetMaxGameMode(response.maxGameMode);
                localPlayer.SetPrivilegeLevel(response.privilegeLevel);
                localPlayer.SetPoints(response.points);

                // TODO: Send the player's character list
                GameService.PushTask(new SendPlayerFactionTask(localPlayer));
            });
        }
    }
}