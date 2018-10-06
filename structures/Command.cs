using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using ShitRP.structures.interfaces;
using ShitRP.util;
using System;

namespace ShitRP.structures
{
    /// <summary>
    /// Representation of a command
    /// </summary>
    public class Command : ICommand
    {
        /// <summary>
        /// The command name
        /// </summary>
        protected string name;

        /// <summary>
        /// Usage of the command
        /// </summary>
        protected string usage;

        /// <summary>
        /// Example of the usage of the command
        /// </summary>
        protected string example;

        /// <summary>
        /// Required permission level to run the command
        /// </summary>
        protected Permission permLvl;

        /// <summary>
        /// Script object that the command belongs to
        /// </summary>
        protected Script script;

        /// <summary>
        /// Run the command
        /// </summary>
        /// <param name="player">Player that ran the command</param>
        /// <param name="args">Arguments of the command string</param>
        /// <returns>True if the command was successful,
        /// false otherwise</returns>
        public virtual bool run(Client player, string[] args)
        {
            return true;
        }

        /// <summary>
        /// Gets the name of the command
        /// </summary>
        /// <returns>The name of the command</returns>
        public virtual string _name()
        {
            return this.name;
        }

        /// <summary>
        /// Gets the usage for the command
        /// </summary>
        /// <returns>The usage of the command</returns>
        public virtual string _usage()
        {
            return this.usage;
        }

        /// <summary>
        /// Gets the example for the command
        /// </summary>
        /// <returns>The example usage of the command</returns>
        public virtual string _example()
        {
            return this.example;
        }

        /// <summary>
        /// Gets the permission level of the command
        /// </summary>
        /// <returns>The permission required to run the command</returns>
        public virtual Permission _permLvl()
        {
            return this.permLvl;
        }

        /// <summary>
        /// Turns the command into a string
        /// </summary>
        /// <returns>The command name</returns>
        public virtual string toString()
        {
            return this.name;
        }

        /// <summary>
        /// Sends the player a mention of the correct usage including an example
        /// </summary>
        /// <param name="player">Player that used the command wrong</param>
        /// <returns>False since the command was used wrong</returns>
        protected bool wrongUsage(Client player)
        {
            this.script.API.sendChatMessageToPlayer(player, String.Format("~r~~h~Correct usage: ~s~~y~{0}~n~~r~~h~Example: ~s~~y~{1}", this.usage, this.example));
            return false;
        }

        /// <summary>
        /// Sends the player a notice that an error happened
        /// </summary>
        /// <param name="player">Player that tried to run the command</param>
        /// <returns>False since the command had an error</returns>
        protected bool error(Client player)
        {
            this.script.API.sendChatMessageToPlayer(player, "~y~There was an error while running the command! Please try again.~n~Should this fail multiple times contact the support.");
            return false;
        }
    }
}
