using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using ShitRP.structures;
using ShitRP.structures.interfaces;
using ShitRP.util;

namespace ShitRP
{
    public partial class Gamemode : Script
    {
        public Dictionary<string, Type> commands = new Dictionary<string, Type>();

        public Gamemode()
        {
            API.onResourceStart += GamemodeStart;
            API.onChatCommand += CommandHandler;

            SetupCommands();

            Permission permissions = Permission.kick | Permission.tempBan | Permission.manageRole;
            Console.WriteLine(permissions.HasFlag(Permission.kick));
        }

        private void API_onChatCommand(Client sender, string command, GrandTheftMultiplayer.Server.API.CancelEventArgs cancel)
        {
            throw new NotImplementedException();
        }

        public void GamemodeStart()
        {
            API.consoleOutput("ShitRP started.");
        }

        public void CommandHandler(Client player, string inp, GrandTheftMultiplayer.Server.API.CancelEventArgs e)
        {
            e.Cancel = true;
            e.Reason = "CustomHandler";

            string command = inp.Substring(1).Split(' ')[0];
            string[] args = inp.Substring(1 + command.Length).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!commands.ContainsKey(command)) return;

            ICommand instance = (ICommand)Activator.CreateInstance(this.commands[command], new Script[] { this });
            try
            {
                API.consoleOutput(instance._name());
                instance.run(player, args);
            }
            catch ( Exception ex )
            {
                Console.WriteLine(ex);
                API.sendChatMessageToPlayer(player, "There was an error running the command. Please contact the support with the specific time and date of the occurence.");
            }
        }

        public void SetupCommands()
        {
            List<Type> typeList = Assembly.GetExecutingAssembly().GetTypes()
              .Where(t => t.Namespace == "ShitRP.commands")
              .ToList();

            foreach (Type type in typeList)
            {
                API.consoleOutput(type.Name.ToLower());

                this.commands.Add(type.Name.ToLower(), type);
            }
        }
    }
}
