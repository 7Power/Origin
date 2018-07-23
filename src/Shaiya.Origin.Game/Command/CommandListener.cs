using Shaiya.Origin.Game.Model.Entity.Player;

namespace Shaiya.Origin.Game.Command
{
    public interface CommandListener
    {
        void Execute(Character character);
    }
}