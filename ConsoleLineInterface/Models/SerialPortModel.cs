using ConsoleLineInterface.IO;
using ConsoleLineInterface.Models;
using Elyon.Commands;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLineInterface
{
    public class SerialPortModel
    {
        #region Properties
        public string ConnectedPort { get; set; }

        public SerialPort Port { get; set; }

        public bool IsConnected { get; set; }

        public UICommand ClearBuffersCommand { get; }
        public UICommand AutoConnectDisconnectCommand { get; }
        public PortSettingsModel Settings { get; set; }

        public MessagesModel Messages { get; set; }

        public SerialReceiver Receiver { get; set; }

        public SerialSender Sender { get; set; }
        #endregion

        public SerialPortModel()
        {
            Port = new SerialPort();
            Port.ReadTimeout = 1000;
            Port.WriteTimeout = 1000;
            Settings = new PortSettingsModel();

            ClearBuffersCommand = new UICommand(ClearBuffers);
            AutoConnectDisconnectCommand = new UICommand(AutoConnectDisconnect);
        }

        #region Connect & Disconnect
        public void Connect()
        {
            IsConnected = Port.IsOpen;
            if (IsConnected)
            {
                Messages.AddMessage("Port is already open!");
                return;
            }
            // COM1 is a system com port and can't be used
            if (Settings.SelectedCOMPort == "COM1")
            {
                Messages.AddMessage("Cannot use COM1!");
                return;
            }

            if (string.IsNullOrEmpty(Settings.SelectedCOMPort))
            {
                Messages.AddMessage("Error with the COM port");
            }
            Port.PortName = Settings.SelectedCOMPort;

            try
            {
                Port.Open();
            }
            catch (Exception e)
            {
                // stacktrace is cooler ;)
                Messages.AddMessage("Error opening port: " + e.Message);
                return;
            }
            ConnectedPort = Settings.SelectedCOMPort;
            Messages.AddMessage($"Connected to {ConnectedPort}!");

            IsConnected = Port.IsOpen;
            Receiver.CanReceive = true;

            RequestData();
        }

        public void Disconnect()
        {
            IsConnected = Port.IsOpen;
            if (!IsConnected)
            {
                Messages.AddMessage("Port is already closed!");
                return;
            }

            try
            {
                Port.Close();
            }
            catch (Exception e)
            {
                // stacktrace is cooler ;)
                Messages.AddMessage("Error closing port: " + e.Message);
                return;
            }

            Messages.AddMessage($"Disconnected from {ConnectedPort}!");
            ConnectedPort = "(None)";
            // Stops it using resources... sort of
            IsConnected = Port.IsOpen;
            Receiver.CanReceive = false;
        }
        #endregion
        private async void RequestData()
        {
            // Get RPM
            Sender.SendMessage(IOCommand.RPMCommand);
            await Task.Delay(10000);

            // Get Coolant Temp
            //Sender.SendMessage("01 05");
            //await Task.Delay(10000);

            //// Get Speed
            //Sender.SendMessage("01 0D");
            //await Task.Delay(10000);

            //// Get Throttle Position
            //Sender.SendMessage("01 48");
        }

        #region Utils
        public void CloseAll()
        {
            Disconnect();
            Receiver.StopThreadLoop();
        }

        private void ClearBuffers()
        {
            if (!Port.IsOpen)
            {
                Messages.AddMessage("You need to be connected to clear the buffers");
                return;
            }

            Port.DiscardInBuffer();
            Port.DiscardOutBuffer();
        }

        public void AutoConnectDisconnect()
        {
            IsConnected = Port.IsOpen;
            if (IsConnected)
            {
                Disconnect();
            }
            else
            {
                Connect();
            }
        }
        #endregion
    }
}
