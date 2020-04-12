using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
using FlightSimulatorApp.Views;
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

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        public FlightViewModel flightSimulatorViewModel;
        public MyFlightModel model;

        private void Application_Startup(Object sender, StartupEventArgs e)
        {
            model = new MyFlightModel(new MyTelnetClient());

            flightSimulatorViewModel = new FlightViewModel(model);
            model.connect("127.0.0.1", 5402);

            Window mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
