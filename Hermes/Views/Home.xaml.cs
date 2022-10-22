using Elyon;
using Elyon.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Hermes.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        readonly DispatcherTimer timer = new DispatcherTimer();
        private static ElyonModel ElyonModel;

        public Home()
        {
            InitializeComponent();
            StartTimer();
            ElyonModel = new ElyonModel(new SerialReceiver());
            DataContext = ElyonModel;
            //RequestData();
        }

        private void RequestData()
        {
            ElyonModel.Sender.SendMessage(IOCommand.RPMCommand);
        }

        private void StartTimer()
        {
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval =  new TimeSpan(0, 0, 0, 1);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            TimeLabel.Content = date.ToLongTimeString();
            DayLabel.Content = date.DayOfWeek.ToString();
            DateLabel.Content = date.ToShortDateString();
        }
    }
}
