using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using ShitRP.structures.interfaces;
using ShitRP.structures;
using ShitRP.util;

namespace ShitRP
{
    public partial class Gamemode : Script
    {
        public Dictionary<string, Type> commands = new Dictionary<string, Type>();
        public Dictionary<string, Player> players = new Dictionary<string, Player>();

        public Gamemode()
        {
            API.onResourceStart += GamemodeStart;
            API.onChatCommand += CommandHandler;
            API.onPlayerConnected += PlayerConnected;

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
                instance.run(this.players[player.name], args);
            }
            catch (Exception ex)
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
                if (type.ToString().Contains("+")) continue;

                Console.WriteLine(type);
                this.commands.Add(type.Name.ToLower(), type);
            }

            API.consoleOutput("{0} commands loaded", this.commands.Count);
        }

        public void PlayerConnected(Client player)
        {
            this.players.Add(player.name, new Player(player));
        }
    }
}
