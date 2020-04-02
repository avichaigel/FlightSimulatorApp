using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        public Joystick()
        {
            InitializeComponent();
        }
        //private void CenterKnob_Completed(object sender, EventArgs e) { }
        private void centerKnob_Completed(object sender, EventArgs e) { }
        private Point FirstPoint = new Point();
        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) {FirstPoint = e.GetPosition(this); }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double deltaX = e.GetPosition(this).X - FirstPoint.X;
                double deltaY = e.GetPosition(this).Y - FirstPoint.Y;
                if (Math.Sqrt(deltaX* deltaX + deltaY*deltaY) < Base.Width / 2)
                {
                    knobPosition.X = deltaX;
                    knobPosition.Y = deltaY;
                }
            }
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
        }
    }
}
