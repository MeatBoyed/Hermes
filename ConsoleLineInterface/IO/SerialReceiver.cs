using ConsoleLineInterface.Models;
using ConsoleLineInterface.Utils;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleLineInterface.IO
{
    // It is the Process Message's task to always create the IOResponse object for enhanced communication within the program.
	public class SerialReceiver : ElyonReceiver
	{
		public override void ProcessMessage(string message)
		{
            if (message.Contains("41 05"))
                DashboardData.AddCoolantTemp(message);
            else if (message.Contains("41 0C"))
                DashboardData.AddVehicleRPM(message);
            else if (message.Contains("41 0D"))
                DashboardData.AddVehicleSpeed(message);
            else if (message.Contains("41 48"))
                DashboardData.AddThrottlePosition(message);
            else
                Messages.AddMessage(message);
        }
    }
}
