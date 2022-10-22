// Use this code inside a project created with the Visual C# > Windows Desktop > Console Application template.
// Replace the code in Program.cs with this code.

using Elyon;
using System;
using System.IO.Ports;
using System.Linq.Expressions;
using System.Threading;


// 1 - Formatting is out ">LM327 v1.5" should be, ">ELM327 v1.5"
// 2 - Request Engine RPM and display both Rest and new command
namespace ConsoleLineInterface
{
    public class Program
    {

    private static SerialPort serialPort;

        // int speed = Serial.Send(OBD-Speed-Command-Object)
        // Serial.Send(IOCommand)
        // Send() => Data Event => WakeUp => Read => Return
        public static void Main(string[] args)
        {
            serialPort = new SerialPort("COM11", 9600, Parity.None, 8, StopBits.One);

            // Setup SerialPort Connection
            serialPort.ReadTimeout = 200;
            serialPort.WriteTimeout = 200;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataRec);

            // Open the Connection
            serialPort.Open();

            // Write Reset Command "AT Z"
            //serialPort.Write("AT Z\r");

            // Read Response "ELM327"
            serialPort.Write("01 0C\r");

            Thread.Sleep(200);

            serialPort.Write("01 05\r");

            // Display Response
            Console.Read();

        }

        private static void DataRec(Object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string msg = serialPort.ReadExisting();
                DisplayMessage(msg);
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        private static void DisplayMessage(string msg)
        {
            Console.WriteLine(msg);
        }

        }
    }
