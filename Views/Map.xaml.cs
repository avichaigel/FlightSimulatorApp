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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class Map : UserControl
    {
        private double _latitude, _longtitude;
        private double newLat, newLong;
        private double latDelta, longDelta;
        private double deg, newDeg;

        public Map()
        {
            InitializeComponent();
            _latitude = 0; _longtitude = 0; newLat = 0; newLong = 0;
            latDelta = 0; longDelta = 0; deg = 0; newDeg = 0;
            DataContext = (Application.Current as App).MapVM;
        }

        private void PlaneRotate()
        {
            if (longDelta != 0)
            {
                newDeg = Math.Atan(latDelta / longDelta) * (180 / Math.PI);
                var doubleAnimation = new DoubleAnimation(deg, newDeg, new Duration(TimeSpan.FromSeconds(1)));
                var rotateTransform = new RotateTransform();
                planeIcon.RenderTransform = rotateTransform;
                planeIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
                rotateTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            }
        }

        private void latitude_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            //newLat = Double.Parse(latitude.Text);
            //latDelta = newLat - _latitude;
            //_latitude = newLat;
            PlaneRotate();
        }

        private void longitude_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            // the longtitude from the map and not the local one!
            //newLong = Double.Parse(longitude.Text);
            //longDelta = newLong - _longtitude;
            //_longtitude = newLong;
            PlaneRotate();
        }
    }

}
