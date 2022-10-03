using Hermes.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hermes.Models
{
    public class PortSettingsViewModel : BaseModel
    {
        private string selectedPort;
        public string SelectedPort
        {
            get => selectedPort;
            set => RaisePropertyChanged(ref selectedPort, value);
        }

        public ObservableCollection<string> AvailblePorts { get; set; }

        // Optional - Connect Front-end to this command
        public Command RefreshPortsCommand { get; }

        public PortSettingsViewModel()
        {
            AvailblePorts = new ObservableCollection<string>();
            RefreshPortsCommand = new Command(RefreshPorts);

            RefreshPorts();
        }

        private void RefreshPorts()
        {
            AvailblePorts.Clear();

            foreach(string port in SerialPort.GetPortNames())
                AvailblePorts.Add(port);
        }
    }
}
