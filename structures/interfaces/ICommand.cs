using ShitRP.util;
using ShitRP.structures;

namespace ShitRP.structures.interfaces
{
    public interface ICommand
    {
        bool run(Player player, string[] args);
        string _name();
        string _usage();
        string _example();
        Permission _permLvl();
        string _category();
        string toString();
    }
}
