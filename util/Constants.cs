using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShitRP.util
{
    [Flags]
    public enum Permission : ushort
    {
        none = 0 << 0,
        mute = 1 << 0,
        kick = 1 << 1,
        teleport = 1 << 2,
        tempBan = 1 << 3,
        ban = 1 << 4,
        manageGun = 1 << 5,
        manageVehicle = 1 << 6,
        manageFaction = 1 << 7,
        manageHouses = 1 << 8,
        managePlayer = 1 << 9,
        manageRole = 1 << 10
    }

    [Flags]
    public enum License : byte
    {
        motorcycle = 1 << 0,
        car = 1 << 1,
        bus = 1 << 2,
        truck = 1 << 3,
        plane = 1 << 4,
        boat = 1 << 5,
        passenger = 1 << 6
    }
}
