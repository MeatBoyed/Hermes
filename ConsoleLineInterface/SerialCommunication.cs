using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLineInterface
{
    public class SerialCommunication
    {
        private SerialPort serialPort;
        private int value;
        
        public SerialCommunication(string port)
        {
            serialPort = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);
            serialPort.ReadTimeout = 200;
            serialPort.WriteTimeout = 200;

            serialPort.DataReceived += new SerialDataReceivedEventHandler(RecievedData);
        }

        private void RecievedData(Object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string response = serialPort.ReadExisting();

            }catch (Exception error)
            {
                Console.WriteLine("Error: ", error);
            }
        }

        private void SaveData(string response)
        {
            string repsonseCode = response.Substring(0, 2);
            string PID = response.Substring(2, 2);
            string value = response.Substring(4, 10);
        }
    }
}
