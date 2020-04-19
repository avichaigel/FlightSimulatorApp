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

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for ErrorsAndConnection.xaml
    /// </summary>
    public partial class ErrorsAndConnection : UserControl
    {
        private ConnectionBox cb;
        private ErrorBox eb;
        public ErrorsAndConnection()
        {
            InitializeComponent();
            DataContext = (Application.Current as App).connectNErrorVM;
            cb = new ConnectionBox((Application.Current as App).connectNErrorVM);
            eb = new ErrorBox((Application.Current as App).connectNErrorVM);
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

        private void Error_Click(object sender, RoutedEventArgs e)
        {

            if (!eb.IsLoaded)
            {
                eb = new ErrorBox((Application.Current as App).connectNErrorVM);
                eb.Show();
            }
        }
    }
}
