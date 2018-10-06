using GrandTheftMultiplayer.Server.Elements;
using ShitRP.structures.interfaces;
using ShitRP.util;
using System;
using System.Collections.Generic;

namespace ShitRP.structures
{
    /// <summary>
    /// Class representing a player inside the ShitRP gamemode
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Client object representing the GTMP player
        /// </summary>
        private Client player { get; }

        /// <summary>
        /// Level of the player
        /// </summary>
        private byte lvl { get; set; }

        /// <summary>
        /// Payday time count
        /// </summary>
        private byte payday { get; set; }

        /// <summary>
        /// Money of the player
        /// </summary>
        private ulong money { get; set; }

        /// <summary>
        /// Bank balance of the player
        /// </summary>
        private ulong bank { get; set; }

        /// <summary>
        /// Wanted level of the player
        /// </summary>
        private byte wantedlvl { get; set; }

        /// <summary>
        /// Role of the player
        /// </summary>
        private IRole role { get; set; }

        /// <summary>
        /// Donator status of the player
        /// </summary>
        private bool donator { get; set; }

        /// <summary>
        /// Licenses of the player
        /// </summary>
        private License licenses { get; set; }

        /// <summary>
        /// Job of the player
        /// </summary>
        private IJob job { get; set; }

        /// <summary>
        /// Faction of the player
        /// </summary>
        private IFaction faction { get; set; }

        /// <summary>
        /// Inventory of the player
        /// </summary>
        private List<IItem> inventory { get; set;  }

        /// <summary>
        /// Vehicles of the player
        /// </summary>
        private List<IVehicle> vehicles { get; set;  }

        /// <summary>
        /// Houses of the player
        /// </summary>
        private List<IHouse> houses { get; set;  }

        /// <summary>
        /// Phonebook of the player
        /// </summary>
        private Dictionary<string, string> phonebook { get; set; }

        /// <summary>
        /// Constructor of the Player class
        /// </summary>
        /// <param name="player">Client object representing the GTMP player</param>
        public Player(Client player)
        {
            this.player = player;
        }

        /// <summary>
        /// Checks if a player can afford a certain price tag
        /// </summary>
        /// <param name="price">The price of the item/object</param>
        /// <returns>True if the player can afford it,
        /// false if not</returns>
        public bool canAfford(ulong price)
        {
            return this.money >= price ? true : false;
        }

        /// <summary>
        /// Adds a specified amount of money to the player
        /// </summary>
        /// <param name="amount">Amount of money to add</param>
        public void addMoney(ulong amount)
        {
            if (amount == 0) return;
            ulong difference = UInt64.MaxValue - this.money;
            this.money += difference < amount ?  difference : amount;
        }

        /// <summary>
        /// Removes a specified amount of money from the player
        /// </summary>
        /// <param name="amount">Amount of money to remove</param>
        public void removeMoney(ulong amount)
        {
            if (amount == 0) return;
            this.money -= this.money < amount ? this.money : amount;
        }

        /// <summary>
        /// Checks if the player has a certain amount of bank balance
        /// </summary>
        /// <param name="amount">Amount of balance to check</param>
        /// <returns>True if the player has the same amount or more,
        /// false otherwise</returns>
        public bool hasBalance(ulong amount)
        {
            return this.bank >= amount ? true : false;
        }

        /// <summary>
        /// Adds a specified amount of money to the players bank account
        /// </summary>
        /// <param name="amount">Amount to add to the bank balance</param>
        public void addBalance(ulong amount)
        {
            if (amount == 0) return;
            ulong difference = UInt64.MaxValue - this.bank;
            this.bank += difference < amount ? difference : amount;
        }

        /// <summary>
        /// Removes a specified amount of money from the player bank account
        /// </summary>
        /// <param name="amount">Amount to remove from the bank balance</param>
        public void removeBalance(ulong amount)
        {
            if (amount == 0) return;
            this.bank -= this.money < amount ? this.bank : amount;
        }

        /// <summary>
        /// Adds a specified amount of wanted levels to the player
        /// </summary>
        /// <param name="amount">Amount to add to the player</param>
        public void addWanted(byte amount)
        {
            if (amount == 0) return;
            byte difference = (byte)(Byte.MaxValue - this.wantedlvl);
            this.wantedlvl += difference < amount ? difference : amount;
        }

        /// <summary>
        /// Removes a specified amount of wanted levels to the player
        /// </summary>
        /// <param name="amount">Amount to remove from the player</param>
        public void removeMoney(byte amount)
        {
            if (amount == 0) return;
            this.wantedlvl -= this.wantedlvl < amount ? this.wantedlvl : amount;
        }

        /// <summary>
        /// Adds a house to the players houses
        /// </summary>
        /// <param name="house">House to add</param>
        public void addHouse(IHouse house)
        {
            this.houses.Add(house);
        }

        /// <summary>
        /// Remove a house from the players houses
        /// </summary>
        /// <param name="house">House to remove</param>
        public void removeHouse(IHouse house)
        {
            this.houses.Remove(house);
        }

        /// <summary>
        /// Adds a vehicle to the players vehicles
        /// </summary>
        /// <param name="vehicle">Vehicle to add</param>
        public void addVehicle(IVehicle vehicle)
        {
            this.vehicles.Add(vehicle);
        }

        /// <summary>
        /// Removes a vehicle from the players vehicles
        /// </summary>
        /// <param name="vehicle">Vehicle to remove</param>
        public void removeVehicle(IVehicle vehicle)
        {
            this.vehicles.Remove(vehicle);
        }

        /// <summary>
        /// Adds an item to the players inventory
        /// </summary>
        /// <param name="item">Item to add</param>
        public void addItem(IItem item)
        {
            this.inventory.Add(item);
        }

        /// <summary>
        /// Removes an item from the players inventory
        /// </summary>
        /// <param name="item">Item to remove</param>
        public void removeItem(IItem item)
        {
            this.inventory.Remove(item);
        }

        /// <summary>
        /// Adds a contact to the players phonebook
        /// </summary>
        /// <param name="alias">Alias of the contact</param>
        /// <param name="number">Number of the contact</param>
        public void addNumber(string alias, string number)
        {
            this.phonebook.Add(number, alias);
        }

        /// <summary>
        /// Removes a contact from the players phonebook
        /// </summary>
        /// <param name="number">Number of the contact</param>
        public void removeNumber(string number)
        {
            this.phonebook.Remove(number);
        }

        /// <summary>
        /// Gets the permissions of the players role
        /// </summary>
        /// <returns>Permissions object of the role</returns>
        public Permission getPermissions()
        {
            return role.getPermissions();
        }

        /// <summary>
        /// Adds a license to the players licenses
        /// </summary>
        /// <param name="license">License to add</param>
        public void addLicense(License license)
        {
            if (this.licenses.HasFlag(license)) return;

            this.licenses = this.licenses | license;
        }

        /// <summary>
        /// Removes a license from the players licenses
        /// </summary>
        /// <param name="license">License to remove</param>
        public void removeLicense(License license)
        {
            if (!this.licenses.HasFlag(license)) return;

            this.licenses = this.licenses ^ license;
        }
    }
}
