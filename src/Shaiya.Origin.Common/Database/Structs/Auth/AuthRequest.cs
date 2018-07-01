using ProtoBuf;

namespace Shaiya.Origin.Common.Database.Structs.Auth
{
    /// <summary>
    /// Represents an authentication request
    /// </summary>
    [ProtoContract(SkipConstructor = true)]
    public class AuthRequest
    {
        [ProtoMember(1)]
        public byte[] username = new byte[18];

        [ProtoMember(2)]
        public byte[] password = new byte[16];

        [ProtoMember(3)]
        public byte[] ipAddress = new byte[15];
    }
}