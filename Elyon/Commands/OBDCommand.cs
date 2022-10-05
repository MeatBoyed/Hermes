using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elyon.Commands
{
    public class OBDCommand : IOCommand
    {
        public OBDCommand(string command) : base(command) { }
    }
}
