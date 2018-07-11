using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shaiya.Origin.Common.Database.Structs.Game
{
    /// <summary>
    /// Represents a request to create a character.
    /// </summary>
    [ProtoContract(SkipConstructor = true)]

    public class CreateCharacterRequest
    {
        // An unknown value
        [ProtoMember(1)]
        public byte unknown;

        [ProtoMember(2)]
        public byte race;

        [ProtoMember(3)]
        public byte mode;

        [ProtoMember(4)]
        public byte hair;

        [ProtoMember(5)]
        public byte face;

        [ProtoMember(6)]
        public byte height;

        [ProtoMember(7)]
        public byte profession;

        [ProtoMember(8)]
        public byte sex;

        [ProtoMember(9)]
        public byte[] name = new byte[19];

    }
}
