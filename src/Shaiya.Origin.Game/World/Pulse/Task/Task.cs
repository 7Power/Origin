namespace Shaiya.Origin.Game.World.Pulse.Task
{
    /// <summary>
    /// Represents a task to be processed by the <see cref="GamePulseHandler"/>.
    /// </summary>
    public abstract class Task
    {
        public abstract void Execute();
    }
}