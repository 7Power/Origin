namespace Shaiya.Origin.Game.Model.Entity
{
    /// <summary>
    /// Represents the position of a game entity (ie Mob, Character, NPC).
    /// </summary>
    public class Position
    {
        public short map { get; set; }

        public float x { get; set; }
        public float y { get; set; }
        public float height { get; set; }

        public short direction { get; set; }

        public Position()
        {
            Set(0, 0, 0, 0, 0);
        }

        public Position(short map, float x, float y, float height, short direction)
        {
            Set(map, x, y, height, direction);
        }

        /// <summary>
        /// Sets the values of the current position, to another position.
        /// </summary>
        /// <param name="position">The position</param>
        public void Set(Position position)
        {
            Set(position.map, position.x, position.y, position.height, position.direction);
        }

        /// <summary>
        /// Sets the values of the current position.
        /// </summary>
        /// <param name="map">The map id</param>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <param name="height">The height (z) coordinate</param>
        /// <param name="direction">The direction to face</param>
        public void Set(short map, float x, float y, float height, short direction)
        {
            SetMap(map);
            SetXYHeight(x, y, height);
            SetDirection(direction);
        }

        /// <summary>
        /// Sets the map value of the current position.
        /// </summary>
        /// <param name="map">The map</param>
        public void SetMap(short map)
        {
            this.map = map;
        }

        /// <summary>
        /// Sets the x coordinate value
        /// </summary>
        /// <param name="x">The x coordinate</param>
        public void SetX(float x)
        {
            this.x = x;
        }

        /// <summary>
        /// Sets the y coordinate value
        /// </summary>
        /// <param name="y">The y coordinate</param>
        public void SetY(float y)
        {
            this.y = y;
        }

        /// <summary>
        /// Sets the height coordinate value.
        /// </summary>
        /// <param name="height">The height coordinate</param>
        public void SetHeight(float height)
        {
            this.height = height;
        }

        /// <summary>
        /// Sets the direction value.
        /// </summary>
        /// <param name="direction">The direction value</param>
        public void SetDirection(short direction)
        {
            this.direction = direction;
        }

        /// <summary>
        /// A helper method to set both the x and y coordinates.
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        public void SetXY(float x, float y)
        {
            SetX(x);
            SetY(y);
        }

        /// <summary>
        /// A helper method to set both the x, y, and height coordinates.
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <param name="height">The height coordinate</param>
        public void SetXYHeight(float x, float y, float height)
        {
            SetXY(x, y);
            SetHeight(height);
        }
    }
}