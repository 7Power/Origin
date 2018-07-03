using ProtoBuf;

namespace Shaiya.Origin.Common.Database.Structs.Login
{
    /// <summary>
    /// Represents an login request
    /// </summary>
    [ProtoContract(SkipConstructor = true)]
    public class LoginRequest
    {
        [ProtoMember(1)]
        public byte[] username = new byte[18];

        [ProtoMember(2)]
        public byte[] password = new byte[16];

        [ProtoMember(3)]
        public byte[] ipAddress = new byte[15];
    }
}