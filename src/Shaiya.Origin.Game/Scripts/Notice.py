import Shaiya.Origin.Game.Command as Command

class GenericCommandListener(Command.CommandListener):
    
    def Execute(self, character):
        print "hello"

CommandDispatcher.RegisterListener("notice", GenericCommandListener())