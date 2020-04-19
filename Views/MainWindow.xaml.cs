using FlightSimulatorApp.ViewModel;
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
using System.Windows.Shapes;
namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ConnectionBox cb;
        //private ConnectionAndErrorVIewModel errorvm;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new
            {
                myMapControl = (Application.Current as App).MapVM, 
                myDashboard = (Application.Current as App).dashboardVM,
                myControllers = (Application.Current as App).controllersVM,
                myConnection = (Application.Current as App).connectNErrorVM,
            };
            cb = new ConnectionBox((Application.Current as App).connectNErrorVM);
            //errorvm = (Application.Current as App).connectNErrorVM;
        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            
            if (!cb.IsLoaded)
            {
                cb = new ConnectionBox((Application.Current as App).connectNErrorVM);
                cb.Show();
            }
        }

        private void disconnect_Click(object sender, RoutedEventArgs e)
        {
            if ((Application.Current as App).connectNErrorVM.VM_Status == "Connected")
            {
                (Application.Current as App).connectNErrorVM.VM_Disconnect();
            } 
        }
    }
}
