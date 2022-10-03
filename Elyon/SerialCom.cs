using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Elyon
{
    public class SerialCom
    {
        public int BaudRate = 9600;
        public int DataBits = 8;
        public string Comport;
        private bool IsReading = false;
        private IOCommand QuedCommand;
        //private IOResponse Response;
        public StopBits StopBits = StopBits.One;
        public Parity ParityBits = Parity.None;
        private SerialPort SerialPort;
        public event EventHandler<DataReceivedArgs> DataReceived;

        public SerialCom(string comport)
        {
            Comport = comport;
            Init();
        }

        #region Constructors & Init
        public SerialCom(string comport, int baudRate, Parity parityBits, int dataBits, StopBits stopBits)
        {
            Comport = comport;
            BaudRate = baudRate;
            DataBits = dataBits;
            StopBits = stopBits;
            ParityBits = parityBits;
            Init();
        }

        private void Init()
        {
            SerialPort = new SerialPort(Comport, BaudRate, ParityBits, DataBits, StopBits);

            SerialPort.ReadTimeout = 200;
            SerialPort.WriteTimeout = 200;

            //OpenCommunication();
        }

        public void OpenCommunication()
        {
            // Check Serial Port isn't open
            if (SerialPort == null)
                SerialPort = new SerialPort(Comport, BaudRate, ParityBits, DataBits, StopBits);
            else
            {
                if (SerialPort.IsOpen)
                {
                    SerialPort.Close();
                }
            }

            SerialPort.DataReceived += DataReceivedHandler;

            // Ensure Buffer is empty
            try
            {
                SerialPort.Open();
                SerialPort.DiscardInBuffer();
                SerialPort.DiscardOutBuffer();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }

        }
        #endregion

        #region Methods
        public bool GetData(IOCommand command)
        {
            QuedCommand = command;
            try
            {
                SerialPort.Write(command.Command);
                return true;
            }catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return false;
            }
        }
        #endregion

        #region Events
        public void DataReceivedHandler(Object sender, SerialDataReceivedEventArgs e)
        {
            string res = "";
            int bytesToRead = SerialPort.BytesToRead;
            Console.WriteLine("Bytes to Read: " + bytesToRead);

            if (bytesToRead > 0)
            {
                IsReading = true;
                res = SerialPort.ReadExisting();
                IOResponse response = new IOResponse(QuedCommand, res);
                OnDataReceived(response);
            }

            IsReading = false;
        }

        public virtual void OnDataReceived(IOResponse response)
        {
            if (DataReceived != null)
                DataReceived(this, new DataReceivedArgs() { Response = response});
        }
        #endregion

    }

    public class DataReceivedArgs : EventArgs
    {
        public IOResponse Response { get; set; }
    }

}
