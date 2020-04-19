using FlightSimulatorApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for ConnectionBox.xaml
    /// </summary>
    public partial class ConnectionBox : Window
    {

        private string userIpAddress;
        private string userPortNumber;
        private ConnectionAndErrorVIewModel cevm;
        public ConnectionBox(ConnectionAndErrorVIewModel vm)
        {
            InitializeComponent();
            cevm = vm;
            DataContext = cevm;
            portSection.Text = ConfigurationManager.AppSettings.Get("Port");
            ipSection.Text = ConfigurationManager.AppSettings.Get("IP");
        }

        public string GetIp()
        {
            return ipSection.Text;
        }

        public string GetPort()
        {
            return portSection.Text;
        }

        private void connect_server_Click(object sender, RoutedEventArgs e)
        {
            cevm.VM_IP = GetIp();
            cevm.VM_Port = GetPort();
            cevm.VM_Connect();
            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
