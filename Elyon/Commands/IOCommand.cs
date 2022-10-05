using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Elyon
{
    /// <summary>
    /// EXTRACT FROM: Section 2.1 - (Abstract) IOCommand
    /// IOCommand is meant to be used by the user, and a folder, "Commands", should be created to store all the necessary 
    /// commands. This folder can be further broken down into OBD specific commands, and ELM327 specific commands for 
    /// easier access. All commands send to a device require a pre-described End of Line character (EoF) to tell the 
    /// device where the end of the command is. All devices use different EoF, however most use the \n, but ELM327 uses the
    /// "\r" characters. Therefore IOCommand is defaulted to the, "\r", character, but can be changed using the alternative 
    /// constructor.
    /// </summary>
    public abstract class IOCommand
    {
        public string Command = "";
        public string Mode;
        public string PID;
        public string ExpectedResponseMode;
        public string ExpectedResponse = "";
        protected string EoF = "\r";

        public IOCommand(string command)
        {
            CreateCommand(command);
        }

        public IOCommand(string command, string expectedResponse)
        {
            ExpectedResponse = expectedResponse;
            CreateCommand(command); 
        }

        /// <summary>
        /// Parses the Mode and PID of the passed command, and attaches the EoF character
        /// </summary>
        /// <param name="command">Desired Command to execute</param>
        protected virtual void CreateCommand(string command)
        {
            // Parse Mode & PID
            string[] splittedCommand = command.Split(' ');
            Mode = splittedCommand[0];
            PID = splittedCommand[1];

            Command = Mode + " " + PID + EoF;
            ExpectedResponseMode = "4" + Convert.ToInt16(Mode);
        }
    }
}
