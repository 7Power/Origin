using ProtoBuf;

namespace Shaiya.Origin.Common.Database.Structs.Login
{
    /// <summary>
    /// Represents the response to an <see cref="LoginRequest"/>
    /// </summary>
    [ProtoContract(SkipConstructor = true)]
    public class LoginResponse
    {
        // The user id
        [ProtoMember(1)]
        public int userId;

        // The status of the user (result of login request, ie valid, banned, invalid password)
        [ProtoMember(2)]
        public int status;

        [ProtoMember(3)]
        public int privilegeLevel;

        [ProtoMember(4)]
        public byte[] identityKeys = new byte[16];
    }
}