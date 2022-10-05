using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elyon.Commands
{
    public class ELM327Commands : IOCommand
    {
        public ELM327Commands(string command, string expectedResponse) : base(command, expectedResponse) { }

        /// <summary>
        /// Parses the Mode and PID of the passed command, and attaches the EoF character
        /// </summary>
        /// <param name="command">Desired Command to execute</param>
        protected override void CreateCommand(string command)
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
