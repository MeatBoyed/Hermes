using ConsoleLineInterface.Models;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleLineInterface.Utils
{
    public abstract class ElyonReceiver
    {
        private Thread ReceiverThread { get; set; }
        public bool CanReceive { get; set; }
        public bool ShouldShutDownPermanently { get; set; }

        public SerialPort Port { get; set; }

        // Easier to hold a reference of the Messages viewmodel instead of creating a few callback methods
        public MessagesModel Messages { get; set; }
        public DashboardData DashboardData { get; set; }

        public ElyonReceiver()
        {
            CanReceive = true;
            ShouldShutDownPermanently = false;

            ReceiverThread = new Thread(ReceiveLoop);
            ReceiverThread.Start();
        }

        /// <summary>
        /// Constantly runs through the entire duration of the program being open... to receive messages
        /// </summary>
        private void ReceiveLoop()
        {
            // Will act like a "receiver buffer", and is better than creating a new string every loop
            string message = "";
            char read;

            while (true)
            {
                // Used for when exiting the application
                if (ShouldShutDownPermanently)
                {
                    return;
                }

                // Used for pausing/resuming the receiver
                if (CanReceive)
                {
                    if (Port != null && Port.IsOpen)
                    {
                        while (Port.BytesToRead > 0)
                        {
                            read = (char)Port.ReadChar();
                            switch (read)
                            {
                                // ElM Command
                                case '\r':
                                    ProcessMessage(message);
                                    Messages.AddReceivedMessage(message);

                                    message = "";
                                    break;
                                // Human command
                                case '\n':
                                    // New Line reached. This will be classed as a new message
                                    Messages.AddReceivedMessage(message);

                                    ProcessMessage(message);

                                    message = "";
                                    break;
                                default:
                                    // Add the read char to the "buffer"
                                    message += read;
                                    break;
                            }
                        }
                    }
                }

                // Stops the thread looping millions of times per second which eats up CPU
                Thread.Sleep(1);
            }
        }

        public abstract void ProcessMessage(string message);

        public void StopThreadLoop()
        {
            CanReceive = false;
            ShouldShutDownPermanently = true;
            // i think abort stops it from running.......
            ReceiverThread.Abort();
        }
    }
}
