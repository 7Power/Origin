using Shaiya.Origin.Common.Networking.Packets;
using Shaiya.Origin.Common.Serializer;
using Shaiya.Origin.Game.Model.Entity.Player;
using Shaiya.Origin.Game.IO.Packets.Outgoing;
using System.Linq;
using System.Text;

namespace Shaiya.Origin.Game.World.Pulse.Task.Impl
{
    public class LoadCharacterGameWorldTask : Task
    {
        private Player _player;
        private int _characterId;

        /// <summary>
        /// The constructor for this task, which will define the player instance
        /// to operate on.
        /// </summary>
        /// <param name="player">The player instance.</param>
        /// <param name="name">The character id</param>
        public LoadCharacterGameWorldTask(Player player, int characterId)
        {
            _player = player;
            _characterId = characterId;
        }

        /// <summary>
        /// Begin loading the character from the database.
        /// </summary>
        public override void Execute()
        {
            LoadCharacterData();
        }

        public void LoadCharacterData()
        {
            Character character = new Character(_player, _characterId);

            Common.Networking.Client.OriginClient dbClient = GameService.GetDbClient();

            PacketBuilder bldr = new PacketBuilder(Common.Database.Opcodes.LOAD_GAME_CHARACTER);

            bldr.WriteInt(_player.index);
            bldr.WriteInt(_characterId);
            bldr.WriteInt(GameService.GetServerId());

            dbClient.Write(bldr.ToPacket(), (_data, _length) =>
            {
                byte response = (_data[0]);

                // If the response was not 0 (0 is success), then we delete the character instance and cancel loading
                if (response != 0)
                {
                    PacketBuilder responseBldr = new PacketBuilder(Common.Packets.Opcodes.CHARACTER_SELECTION);

                    // Write a failed opcode
                    responseBldr.WriteByte(response);

                    responseBldr.WriteByte((byte)character.GetIndex());

                    character.Write(responseBldr.ToPacket());

                    return;
                }
                else
                {
                    PacketBuilder responseBldr = new PacketBuilder(Common.Packets.Opcodes.CHARACTER_SELECTION);

                    responseBldr.WriteByte(response);

                    responseBldr.WriteByte((byte)character.GetIndex());

                    Common.Database.Structs.Game.CharacterLoadInfo info = new Common.Database.Structs.Game.CharacterLoadInfo();

                    info = Serializer.Deserialize<Common.Database.Structs.Game.CharacterLoadInfo>(_data.Skip(1).ToArray());

                    character.name = Encoding.UTF8.GetString(info.name).TrimEnd('\0');

                    character.profession = info.profession;
                    character.race = info.race;
                    character.mode = info.mode;

                    character.statPoints = info.statPoints;
                    character.skillPoints = info.skillPoints;
                    character.gold = info.gold;
                    character.kills = info.kills;
                    character.deaths = info.deaths;
                    character.victories = info.victories;
                    character.defeats = info.defeats;
                    character.guildId = info.guildId;

                    character.GetPosition().Set(info.mapId, info.positionX, info.positionY, info.positionHeight, info.direction);

                    var attributes = character.GetAttributes();

                    attributes.level = info.level;
                    attributes.strength = info.strength;
                    attributes.dexterity = info.dexterity;
                    attributes.resistance = info.resistance;
                    attributes.intelligence = info.intelligence;
                    attributes.wisdom = info.wisdom;
                    attributes.luck = info.luck;
                    attributes.currentHp = info.currentHp;
                    attributes.currentMp = info.currentMp;
                    attributes.currentSp = info.currentSp;

                    character.Write(responseBldr.ToPacket());

                    // TODO: Start loading character's details, skills, items, buffs, bars etc...
                    PacketRepository.SendCharacterData(character);
                }
            });
        }
    }
}