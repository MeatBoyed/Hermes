using Hermes.API;
using Hermes.Models;
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

        public Home()
        {
            InitializeComponent();
            StartTimer();
            DataContext = new HomeViewModel();
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
