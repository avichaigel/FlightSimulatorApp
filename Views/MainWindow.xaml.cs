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
        private ErrorBox eb;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new
            {
                myMapControl = (Application.Current as App).MapVM, 
                myDashboard = (Application.Current as App).dashboardVM,
                myConnection = (Application.Current as App).connectNErrorVM,
                myControllers = (Application.Current as App).controllersVM,
            };
            cb = new ConnectionBox((Application.Current as App).connectNErrorVM);
            eb = new ErrorBox((Application.Current as App).connectNErrorVM);
        }

        

       
    }
}
