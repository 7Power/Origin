namespace Shaiya.Origin.Game.Model.Entity
{
    /// <summary>
    /// Represents an entity that can interact with the game world (ie all mobs, characters, npcs).
    /// </summary>
    public class Entity
    {
        // The index of the entity (character id, mob unique index, etc)
        private int _index;

        private Attributes _attributes = new Attributes();
        private Position _position = new Position();
        private UpdateFlags _updateFlags = new UpdateFlags();

        public Entity(int index)
        {
            _index = index;
        }

        public string name { get; set; }

        /// <summary>
        /// Gets the index for this entity.
        /// </summary>
        /// <returns>The unique index</returns>
        public int GetIndex()
        {
            return _index;
        }

        /// <summary>
        /// Gets the attributes for this entity.
        /// </summary>
        /// <returns>The attribute</returns>
        public Attributes GetAttributes()
        {
            return _attributes;
        }

        /// <summary>
        /// Gets the position of this entity.
        /// </summary>
        /// <returns>The position</returns>
        public Position GetPosition()
        {
            return _position;
        }

        /// <summary>
        /// Gets the update flags of this entity.
        /// </summary>
        /// <returns>The update flags</returns>
        public UpdateFlags GetUpdateFlags()
        {
            return _updateFlags;
        }
    }
}