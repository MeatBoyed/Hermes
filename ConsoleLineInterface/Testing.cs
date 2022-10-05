using Elyon;
using Elyon.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleLineInterface
{
    public class Testing
    {
        public static void Main(string[] args)
        {
            OBDCommand RPMCommand = new OBDCommand("01 0C");
            IOQuery query = new IOQuery(RPMCommand);
            IODevice device = new IODevice("COM11");
            device.QueryCompleted += OnQueryCompleted;

            Console.WriteLine("Command: " + RPMCommand.Command);

            Console.WriteLine("Excuting Command");
            bool result = device.RequestData(query);
            if (result)
                Console.WriteLine("Result: True");
            else
                Console.WriteLine("Result: False");


            Console.WriteLine("------------------------------------------------------------------------");
            Console.ReadLine();
        }

        public static void OnQueryCompleted(object sender, QueryCompletedArgs data)
        {
            IOQuery queryResponse = data.Query;

            Console.WriteLine("Raw Response: " + queryResponse.Response.RawResponse);
            Console.WriteLine("Response CODE: " + queryResponse.Response.ResponseCode);
            Console.WriteLine("PID: " + queryResponse.Response.PID);
            Console.WriteLine("Value: " + queryResponse.Response.Value);
            Console.WriteLine("Error Message: " + queryResponse.Response.ErrorMessage);
        }
    }
}
