using ConsoleLineInterface.IO;
using ConsoleLineInterface.Utils;

namespace ConsoleLineInterface.Models
{
    public class ElyonModel: BaseModel
    {
        public MessagesModel Messages { get; set; }
        public DashboardData DashboardData { get; set; }

        public SerialReceiver Receiver { get; set; }

        public SerialSender Sender { get; set; }

        public SerialPortModel SerialPort { get; set; }

        public ElyonModel()
        {
            Sender = new SerialSender();
            Messages = new MessagesModel();
            Receiver = new SerialReceiver();
            SerialPort = new SerialPortModel();
            DashboardData = new DashboardData();

            // hmm
            Messages.Sender = Sender;
            Receiver.Messages = Messages;
            Receiver.DashboardData = DashboardData;
            Sender.Messages = Messages;

            SerialPort.Receiver = Receiver;
            SerialPort.Sender = Sender;
            SerialPort.Messages = Messages;

            Receiver.Port = SerialPort.Port;
            Sender.Port = SerialPort.Port;
        }
    }
}
