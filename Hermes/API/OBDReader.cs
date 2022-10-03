using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Elyon;
using Hermes.Models;
using Hermes.Utils;
using OBD.NET.Communication;
using OBD.NET.Devices;
using OBD.NET.Extensions;
using OBD.NET.Logging;
using OBD.NET.OBDData;


namespace Hermes.API
{
    public class OBDReader : BaseModel
    {
        private bool isConnected;
        public bool IsConnected
        {
            get => isConnected;
            set => RaisePropertyChanged(ref isConnected, value);
        }

        public PortSettingsViewModel PortSettings { get; set; }
        public Command ConnectCommand { get; }

        public int Pos
        {
            get => 50;
        }
        public OBDReader()
        {
            PortSettings = new PortSettingsViewModel();
            ConnectCommand = new Command(Connect);
        }

        public void Connect()
        {
        }
    }
}
