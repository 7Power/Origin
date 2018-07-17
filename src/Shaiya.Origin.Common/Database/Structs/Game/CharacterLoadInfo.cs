using ProtoBuf;

namespace Shaiya.Origin.Common.Database.Structs.Game
{
    /// <summary>
    /// The character's info information
    /// </summary>
    [ProtoContract(SkipConstructor = true)]
    public class CharacterLoadInfo
    {
        [ProtoMember(1)]
        public byte[] name = new byte[32];

        [ProtoMember(2)]
        public int characterId;

        [ProtoMember(3)]
        public byte profession;
        [ProtoMember(4)]
        public byte race;
        [ProtoMember(5)]
        public byte mode;

        [ProtoMember(6)]
        public short level;

        [ProtoMember(7)]
        public byte hair;
        [ProtoMember(8)]
        public byte face;
        [ProtoMember(9)]
        public byte height;
        [ProtoMember(10)]
        public byte sex;

        [ProtoMember(11)]
        public int strength;
        [ProtoMember(12)]
        public int dexterity;
        [ProtoMember(13)]
        public int resistance;
        [ProtoMember(14)]
        public int intelligence;
        [ProtoMember(15)]
        public int wisdom;
        [ProtoMember(16)]
        public int luck;

        [ProtoMember(17)]
        public int currentHp;
        [ProtoMember(18)]
        public int currentMp;
        [ProtoMember(19)]
        public int currentSp;

        [ProtoMember(20)]
        public short statPoints;
        [ProtoMember(21)]
        public short skillPoints;

        [ProtoMember(22)]
        public int gold;

        [ProtoMember(23)]
        public int kills;
        [ProtoMember(24)]
        public int deaths;
        [ProtoMember(25)]
        public int victories;
        [ProtoMember(26)]
        public int defeats;

        [ProtoMember(27)]
        public int guildId;

        [ProtoMember(28)]
        public short mapId;

        [ProtoMember(29)]
        public byte direction;
        [ProtoMember(30)]
        public float positionX;
        [ProtoMember(31)]
        public float positionY;
        [ProtoMember(32)]
        public float positionHeight;
    }
}