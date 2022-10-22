using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLineInterface.Utils
{
    public class DashboardData : BaseModel
    {
        private long vehicleRPM;
        private long coolantTemp;
        private long vehicleSpeed;
        private long throttlePosition;

        #region Public Properties
        public long VehicleRPM
        {
            get => vehicleRPM; 
            set => RaisePropertyChanged(ref vehicleRPM, value);
        }
        public long CoolantTemp
        {
            get => coolantTemp;
            set => RaisePropertyChanged(ref coolantTemp, value);
        }
        public long VehicleSpeed
        {
            get => vehicleSpeed;
            set => RaisePropertyChanged(ref vehicleSpeed, value);
        }
        public long ThrottlePosition
        {
            get => throttlePosition;
            set => RaisePropertyChanged(ref throttlePosition, value);
        }
        #endregion

        #region Methods
        public void AddVehicleRPM(string message)
        {
            string yes = message.Replace("\\n", "");
            string[] splittedRes = yes.Split(' ');
            VehicleRPM = Int64.Parse(splittedRes[2] + splittedRes[3], System.Globalization.NumberStyles.HexNumber) / 4;
        }
        public void AddCoolantTemp(string message)
        {
            string[] splittedRes = message.Split(' ');
            CoolantTemp = Int64.Parse(splittedRes[2], System.Globalization.NumberStyles.HexNumber) - 40;
        }

        public void AddVehicleSpeed(string message)
        {
            string[] splittedRes = message.Split(' ');
            VehicleSpeed = Int64.Parse(splittedRes[2] + splittedRes[3], System.Globalization.NumberStyles.HexNumber) / 4;
        }

        public void AddThrottlePosition(string message)
        {
            string[] splittedRes = message.Split(' ');
            ThrottlePosition = Int64.Parse(splittedRes[2], System.Globalization.NumberStyles.HexNumber);
        }
        #endregion

    }
}
