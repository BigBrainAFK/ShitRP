using GrandTheftMultiplayer.Server.Elements;
using ShitRP.util;

namespace ShitRP.structures.interfaces
{
    public interface ICommand
    {
        bool run(Client player, string[] args);
        string _name();
        string _usage();
        string _example();
        Permission _permLvl();
        string toString();
    }
}
