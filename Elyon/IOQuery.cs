using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elyon
{
    public class IOQuery
    {
        public IOCommand Command;
        public string ErrorMessage;
        public IOResponse Response { get; set; };

        public IOQuery(IOCommand command)
        {
            Command = command;
        }
    }
}
