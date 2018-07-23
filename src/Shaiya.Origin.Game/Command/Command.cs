using System;
using System.Collections.Generic;
using System.Text;

namespace Shaiya.Origin.Game.Command
{
    public class Command
    {
        private string _commandName;

        public Command(string commandName)
        {
            _commandName = commandName;
        }

        public string GetName()
        {
            return _commandName;
        }
    }
}
