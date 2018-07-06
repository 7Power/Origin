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

        private int _playerId;
        private int _faction;
        private int _privilegeLevel;
        private int _points;
        private int _maxGameMode;
        private bool _selectedCharacter = false;

        public Player(int index, ServerSession session)
        {
            _session = session;
            _playerId = index;
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

        /// <summary>
        /// Gets the faction of this player's account
        /// </summary>
        /// <returns>The faction</returns>
        public int GetFaction()
        {
            return _faction;
        }

        /// <summary>
        /// Gets the privilege level of this player's account
        /// </summary>
        /// <returns>The privilege level</returns>
        public int GetPrivilegeLevel()
        {
            return _privilegeLevel;
        }

        /// <summary>
        /// Gets the maximum selectable game mode for this account
        /// </summary>
        /// <returns>The maximum selectable game mode for character creation</returns>
        public int GetMaxGameMode()
        {
            return _maxGameMode;
        }

        /// <summary>
        /// Gets the faction of this player's account
        /// </summary>
        /// <returns>The faction</returns>
        public int GetPoints()
        {
            return _points;
        }

        /// <summary>
        /// Sets the faction for this player
        /// </summary>
        /// <param name="faction">The faction id</param>
        public void SetFaction(int faction)
        {
            _faction = faction;
        }

        /// <summary>
        /// Sets the privilege level for this player
        /// </summary>
        /// <param name="privilegeLevel">The privilege level</param>
        public void SetPrivilegeLevel(int privilegeLevel)
        {
            _privilegeLevel = privilegeLevel;
        }

        /// <summary>
        /// Sets the maximum selectable game mode
        /// </summary>
        /// <param name="maxGameMode">The max game mode</param>
        public void SetMaxGameMode(int maxGameMode)
        {
            _maxGameMode = maxGameMode;
        }

        /// <summary>
        /// Sets the amount of item-mall points
        /// </summary>
        /// <param name="faction">The points</param>
        public void SetPoints(int points)
        {
            _points = points;
        }

        /// <summary>
        /// Gets the index for this player instance
        /// </summary>
        /// <returns>The player id</returns>
        public int GetIndex()
        {
            return _playerId;
        }

    }
}