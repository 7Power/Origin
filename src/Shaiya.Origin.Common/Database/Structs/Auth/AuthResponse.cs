using ProtoBuf;

namespace Shaiya.Origin.Common.Database.Structs.Auth
{
    /// <summary>
    /// Represents the response to an <see cref="AuthRequest"/>
    /// </summary>
    [ProtoContract(SkipConstructor = true)]
    public class AuthResponse
    {
        // The user id
        [ProtoMember(1)]
        public int userId;

        // The status of the user (result of authentication request, ie valid, banned, invalid password)
        [ProtoMember(2)]
        public int status;

        [ProtoMember(3)]
        public int privilegeLevel;

        [ProtoMember(4)]
        public byte[] identityKeys = new byte[16];
    }
}