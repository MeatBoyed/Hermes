using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLineInterface
{
    public class PortSettingsModel : BaseModel
    {
        private string _selectedComPort;
        public string SelectedCOMPort
        {
            get => _selectedComPort;
            set => RaisePropertyChanged(ref _selectedComPort, value);
        }

        public ObservableCollection<string> AvaliablePorts { get; set; }

        // one of the most common baud rate
        public int BaudRate = 9600;

        // i think determinds how many bits are in 1 byte. so 8. could also use 5.. if you wanted to
        public int DataBits = 8;

        // I think the serialport only supports none, probably
        public StopBits StopBits = StopBits.None;

        // The serialport doesn't support all of the parity options, i think such as mark/even
        public Parity Parity = Parity.None;

        // not using a handshake
        public Handshake Handshake = Handshake.None;

        public UICommand RefreshPortsCommand { get; }

        public PortSettingsModel()
        {
            AvaliablePorts = new ObservableCollection<string>();

            RefreshPortsCommand = new UICommand(RefreshPorts);

            RefreshPorts();
        }

        private void RefreshPorts()
        {
            AvaliablePorts.Clear();
            foreach (string port in SerialPort.GetPortNames())
            {
                AvaliablePorts.Add(port);
            }
        }
    }
}
