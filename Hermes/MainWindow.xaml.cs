using Hermes.Views;
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

namespace Hermes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Artemis());
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            //MainFrame.Navigate(new Home());
            MainFrame.Navigate(new Home());
        }
        private void ArtemisBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Artemis());
        }

        private void ErrorScanBtn_Click(object sender, RoutedEventArgs e)
        {

        }


        private void CANScannerBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
