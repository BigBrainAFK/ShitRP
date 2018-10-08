using System;
using ShitRP.structures;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared;

namespace ShitRP.commands
{
    class GiveGun : Command
    {
        public GiveGun(Script script)
        {
            this.name = "givegun";
            this.usage = "/givegun [player] [gun] [ammo] [equip]";
            this.example = "/givegun Max_Mustermann CombatMGMk2 10 true";
            this.permLvl = util.Permission.manageGun;
            this.category = "Admin";
            this.script = script;
        }

        public override bool run(Player player, string[] args)
        {
            if (args.Length != 4) return this.wrongUsage(player);

            WeaponHash weapon = WeaponHash.Unarmed;
            if (WeaponHash.TryParse<WeaponHash>(args[1], out weapon))
            {
                Console.WriteLine(weapon);
                if (WeaponHash.Unarmed.Equals(weapon)) return this.wrongUsage(player);

                int ammo = 1;
                bool equip = true;
                Client target;

                try
                {
                    target = script.API.getPlayerFromName(args[0]);

                    if (target == null) target = player.client;

                    ammo = int.Parse(args[2]);
                    if (ammo > 9999) return this.wrongUsage(player);

                    equip = bool.Parse(args[3]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return this.wrongUsage(player);
                }

                try
                {
                    target.giveWeapon(weapon, ammo, equip);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return this.error(player);
                }
            }
            else
            {

                return this.wrongUsage(player);
            }

            script.API.sendChatMessageToPlayer(player.client, "Success2");
            return true;
        }
    }
}
