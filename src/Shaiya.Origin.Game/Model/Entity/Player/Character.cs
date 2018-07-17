using Shaiya.Origin.Common.Networking.Packets;

namespace Shaiya.Origin.Game.Model.Entity.Player
{
    /// <summary>
    /// Represents a character instance than can interact with the game world.
    /// </summary>
    public class Character : Entity
    {
        private Player _player;

        // If the character instance is ready to enter the game world
        private bool _initialised = false;

        public Character(Player player, int characterId) : base(characterId)
        {
            _player = player;
        }

        /// <summary>
        ///  Checks if this character instance is initialised.
        /// </summary>
        /// <returns>If the character is initialised</returns>
        public bool IsInitialised()
        {
            return _initialised;
        }

        /// <summary>
        /// Writes the packet to the session.
        /// </summary>
        /// <param name="packet">The packet to write</param>
        public void Write(Packet packet)
        {
            _player.Write(packet);
        }

        public Player GetPlayer()
        {
            return _player;
        }

        public byte profession { get; set; }
        public byte race { get; set; }
        public byte mode { get; set; }

        public byte hair { get; set; }
        public byte face { get; set; }
        public byte height { get; set; }
        public byte sex { get; set; }

        public short statPoints { get; set; }
        public short skillPoints { get; set; }

        public int gold { get; set; }

        public int kills { get; set; }
        public int deaths { get; set; }
        public int victories { get; set; }
        public int defeats { get; set; }

        public int guildId { get; set; }
    }
}