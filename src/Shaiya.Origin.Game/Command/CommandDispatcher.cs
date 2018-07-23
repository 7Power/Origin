using Shaiya.Origin.Game.Model.Entity.Player;
using System.Collections.Generic;

namespace Shaiya.Origin.Game.Command
{
    public class CommandDispatcher
    {
        Dictionary<string, dynamic> commands = new Dictionary<string, dynamic>();

        public void RegisterListener(string commandName, dynamic listener)
        {
            commands.Add(commandName, listener);
        }

        public void Dispatch(Character character, string commandName)
        {
            var listener = commands[commandName];

            listener.Execute(character);
        }
    }
}