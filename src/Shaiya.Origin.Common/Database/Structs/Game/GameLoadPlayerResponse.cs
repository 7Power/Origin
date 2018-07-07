using ProtoBuf;

namespace Shaiya.Origin.Common.Database.Structs.Game
{
    /// <summary>
    /// Represents a response to loading a game account.
    /// </summary>
    [ProtoContract(SkipConstructor = true)]
    public class GameLoadPlayerResponse
    {
        [ProtoMember(1)]
        public int faction;

        [ProtoMember(2)]
        public int maxGameMode;

        [ProtoMember(3)]
        public int privilegeLevel;

        [ProtoMember(4)]
        public int points;
    }
}