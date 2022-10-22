﻿using Elyon;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLineInterface.Models
{
    public class SerialSender
    {
        public bool CanSend { get; set; }
        public SerialPort Port { get; set; }
        public MessagesModel Messages { get; set; }

        public SerialSender()
        {
            CanSend = true;
        }

        /// <summary>
        /// Sends a message through the serial port. This COULD throw exceptions which can be handled somewhere else
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(IOMessage message)
        {

            // Gets the bytes of the message using the serial port's encoding
            // Will be using a byte buffer because it's faster than sending strings using 
            // the serial port's build in methods... apparently
            byte[] buffer = Port.Encoding.GetBytes(message.Message);

            for (int i = 0; i < buffer.Length; i++)
            {
                Port.BaseStream.WriteByte(buffer[i]);
            }
        }
        //public void SendMessage(string message, bool shouldSendNewLine = true)
        //{
        //    // Adds a new line if needed
        //    string newMessage = message + (shouldSendNewLine ? "\n" : "");
        //    // Gets the bytes of the message using the serial port's encoding
        //    // Will be using a byte buffer because it's faster than sending strings using 
        //    // the serial port's build in methods... apparently
        //    byte[] buffer = Port.Encoding.GetBytes(newMessage);

        //    for (int i = 0; i < buffer.Length; i++)
        //    {
        //        Port.BaseStream.WriteByte(buffer[i]);
        //    }
        //}
    }
}
