using ShitRP.structures;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;

namespace ShitRP.commands
{
    class Help : Command
    {
        public Help(Script script)
        {
            this.name = "help";
            this.usage = "/help";
            this.example = "/help";
            this.permLvl = util.Permission.none;
            this.script = script;
        }

        public override bool run(Client player, string[] args)
        {
            script.API.sendChatMessageToPlayer(player, "Success");
            return true;
        }
    }
}
