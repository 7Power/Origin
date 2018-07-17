namespace Shaiya.Origin.Game.Model.Entity
{
    /// <summary>
    /// Represents the attributes of a game entity (ie Mob, Character, NPC).
    /// </summary>
    public class Attributes
    {
        public short level { get; set; }
        public int strength { get; set; }
        public int dexterity { get; set; }
        public int resistance { get; set; }
        public int intelligence { get; set; }
        public int wisdom { get; set; }
        public int luck { get; set; }
        public int currentHp { get; set; }
        public int currentMp { get; set; }
        public int currentSp { get; set; }
        public int currentExperience { get; set; }
    }
}