using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shaiya.Origin.Common.Database.Structs.Game
{
    /// <summary>
    /// Represents a login request
    /// </summary>
    [ProtoContract(SkipConstructor = true)]
    public class GameHandshakeRequest
    {
        [ProtoMember(1)]
        public int userId;

        [ProtoMember(2)]
        public byte[] identityKeys = new byte[16];
    }
}
