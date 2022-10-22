using ConsoleLineInterface.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLineInterface.Models
{
    public class MessagesModel : BaseModel
    {
        // Default "messageText" is used for logging information from the program
        private string messageText;
        private string toBeSentText;
        private int messageCount;


        public string MessageText
        {
            get => messageText;
            set => RaisePropertyChanged(ref messageText, value);
        }

        public string ToBeSentText
        {
            get => toBeSentText;
            set => RaisePropertyChanged(ref toBeSentText, value);
        }

        public int MessageCount
        {
            get => messageCount;
            set => RaisePropertyChanged(ref messageCount, value);
        }

        public UICommand SendMessageCommand { get; }
        public UICommand ClearMessagesCommand { get; }

        public SerialSender Sender { get; set; }
        public DashboardData DasBoardData { get; set; } 

        public MessagesModel()
        {
            MessageText = "";
            ToBeSentText = "";

            SendMessageCommand = new UICommand(SendMessage);
            ClearMessagesCommand = new UICommand(ClearMessages);
        }
        private void ClearMessages()
        {
            MessageText = "";
            MessageCount = 0;
        }


        private void SendMessage()
        {
            if (!string.IsNullOrEmpty(ToBeSentText))
            {
                try
                {
                    //Sender.SendMessage(ToBeSentText);
                    AddSentMessage(ToBeSentText);
                    // Clear text after sending. this is a personal preference ;)
                    ToBeSentText = "";
                }
                catch (TimeoutException timeout)
                {
                    AddMessage("Timeout Exception. Couldn't send message");
                }
                catch (Exception e)
                {
                    AddMessage("Error: " + e.Message);
                }
            }
        }


        public void AddSentMessage(string message)
        {
            // (Date) | TX> hello there
            AddMessage($"{DateTime.Now} | TX> {message}");
        }

        public void AddReceivedMessage(string message)
        {
            // (Date) | RX> hello there
            AddMessage($"{DateTime.Now} | RX> {message}");
        }

        public void AddMessage(string message)
        {
            WriteLine(message);
            MessageCount++;
        }

        public void WriteLine(string text)
        {
            MessageText += text + '\n';
        }

    }
}
