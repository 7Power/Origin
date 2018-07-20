using Shaiya.Origin.Common.Networking.Packets;
using Shaiya.Origin.Common.Networking.Server.Session;

namespace Shaiya.Origin.Game.Model.Entity.Player
{
    /// <summary>
    /// Represents a player's game account instance, which holds reference to the connection session,
    /// and keeps track of the character that is currently in use.
    /// </summary>
    public class Player
    {
        private ServerSession _session;

        public int index { get; set; }
        public int faction { get; set; }
        public int privilegeLevel { get; set; }
        public int points { get; set; }
        public int maxGameMode { get; set; }
        public bool selectedCharacter = false;

        public Player(int index, ServerSession session)
        {
            _session = session;
            this.index = index;
            session.SetGameIndex(index);
        }

        /// <summary>
        /// Forward the write packet function to the player's session
        /// </summary>
        /// <param name="packet">The packet to write</param>
        public void Write(Packet packet)
        {
            _session.Write(packet);
        }
    }
}