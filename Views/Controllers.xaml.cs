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
    /// Interaction logic for Controllers.xaml
    /// </summary>
    public partial class Controllers : UserControl
    {
        public Controllers()
        {
            InitializeComponent();
            Joystick.MoveEvent += NormalizeValues;
            RudderValue.Text = "0";
            ElevatorValue.Text = "0";
            DataContext = (Application.Current as App).controllersVM;
        }

        private void NormalizeValues(double x, double y)
        {
            if (x > 1)
            {
                x = 1;
            }
            else if (x < -1)
            {
                x = -1;
            }
            if (y > 1)
            {
                y = 1;
            }
            else if (y < -1)
            {
                y = -1;
            }
            RudderValue.Text = x.ToString();
            ElevatorValue.Text = y.ToString();
        }

        private void ThrottleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ThrottleValue.Text = ThrottleSlider.Value.ToString("F");
        }

        private void AileronSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            AileronValue.Text = AileronSlider.Value.ToString("F");
        }

    }
}
