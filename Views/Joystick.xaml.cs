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
    public partial class Joystick : UserControl
    {
        // We are using it as a anchor after each movemoent
        private Point centerAnchor;
        // checks (multithreadingly) if our mouse has been touched 
        private volatile bool isClicked;
        private double destX, destY;

        public Joystick()
        {
            InitializeComponent();
            centerAnchor = new Point(Base.Width / 2, Base.Height / 2);

        }
        private void centerKnob_Completed(object sender, EventArgs e) { }
        private Point FirstPoint = new Point();
        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) { FirstPoint = e.GetPosition(this); }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                isClicked = true;
            }
        }

        private void BaseMouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void BaseMouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
