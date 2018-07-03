using ProtoBuf;

namespace Shaiya.Origin.Common.Database.Structs.Game
{
    /// <summary>
    /// Represents a server instance
    /// </summary>
    [ProtoContract(SkipConstructor = true)]
    public class Server
    {
        [ProtoMember(1)]
        public int serverId;

        [ProtoMember(2)]
        public string serverName;

        [ProtoMember(3)]
        public int population;

        [ProtoMember(4)]
        public int status;

        [ProtoMember(5)]
        public string ipAddress;

        [ProtoMember(6)]
        public int maxPlayers;

        [ProtoMember(7)]
        public int clientVersion;
    }
}