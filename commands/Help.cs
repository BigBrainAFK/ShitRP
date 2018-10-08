using ShitRP.structures;
using GrandTheftMultiplayer.Server.API;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;
using ShitRP.structures.interfaces;
using ShitRP.util;

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
            this.category = "General";
            this.script = script;
        }

        public override bool run(Player player, string[] args)
        {
            script.API.sendChatMessageToPlayer(player.client, "Success");
            
            Dictionary<string, List<string>> helpList = new Dictionary<string, List<string>>();
            List<Type> typeList = Assembly.GetExecutingAssembly().GetTypes()
              .Where(t => t.Namespace == "ShitRP.commands")
              .ToList();

            foreach (Type type in typeList)
            {
                if (type.ToString().Contains("+")) continue;
                
                ICommand instance = (ICommand)Activator.CreateInstance(type, new Script[] { this.script });

                //Check for perms

                string category = instance._category();

                Console.WriteLine(category);

                if (!helpList.ContainsKey(category)) helpList.Add(category, new List<string>());
                helpList[category].Add(instance._name());
            }

            foreach (KeyValuePair<string, List<string>> entry in helpList)
            {
                Boolean first = true;
                string[] cmds = entry.Value.ToArray();
                var splitCmds = cmds.Split(8);

                foreach (IEnumerable<string> currentCmds in splitCmds)
                {
                    if (first)
                    {
                        script.API.sendChatMessageToPlayer(player.client, String.Format("~y~{0}: {1}", entry.Key, String.Join(", ", currentCmds.ToArray())));
                        first = !first;
                    }
                    else
                    {
                        script.API.sendChatMessageToPlayer(player.client, String.Format("~y~{0}", String.Join(", ", currentCmds.ToArray())));
                    }
                }
                script.API.sendChatMessageToPlayer(player.client, "~y~-----------------------------------------------------------");
            }
            return true;
        }
    }
}
