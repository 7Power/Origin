using ProtoBuf;

namespace Shaiya.Origin.Common.Database.Structs.Game
{
    /// <summary>
    /// Represents a character.
    /// </summary>
    [ProtoContract(SkipConstructor = true)]

    public class Character
    {
        [ProtoMember(1)]
        public byte slot;

        [ProtoMember(2)]
        public int characterId;

        [ProtoMember(3)]
        public short level;

        [ProtoMember(4)]
        public byte race;

        [ProtoMember(5)]
        public byte gameMode;

        [ProtoMember(6)]
        public byte hair;

        [ProtoMember(7)]
        public byte face;

        [ProtoMember(8)]
        public byte height;

        [ProtoMember(9)]
        public byte profession;

        [ProtoMember(10)]
        public byte sex;

        [ProtoMember(11)]
        public short map;

        [ProtoMember(12)]
        public short strength;
        [ProtoMember(13)]
        public short dexterity;
        [ProtoMember(14)]
        public short resistance;
        [ProtoMember(15)]
        public short intelligence;
        [ProtoMember(16)]
        public short wisdom;
        [ProtoMember(17)]
        public short luck;

        [ProtoMember(18)]
        public byte[] itemTypes = new byte[17];
        [ProtoMember(19)]
        public byte[] itemTypeIds = new byte[17];

        [ProtoMember(20)]
        public byte[] name = new byte[19];

        [ProtoMember(21)]
        public byte isPendingDeletion = 0;
    }
}