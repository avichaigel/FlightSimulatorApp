using System;
using System.Collections.Generic;
using System.Text;
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
    public partial class Joystick : UserControl
    {
        // We are using it as a anchor after each movemoent
        private Point centerAnchor;
        // checks (multithreadingly) if our mouse has been touched 
        private volatile bool isClicked;
        private double destX, destY;
        private double ellipseRadius, norma = 1.02;
        private double normalX = 0, normalY = 0;
        public delegate void Event(double x, double y);
        public Event MoveEvent;

        public Joystick()
        {
            InitializeComponent();
            centerAnchor = new Point(Base.Width / 2, Base.Height / 2);
            isClicked = false;
            ellipseRadius = this.circleFrame.Width / 2;
            Storyboard storyboard = Knob.Resources["CenterKnob"] as Storyboard;
            DoubleAnimation x = storyboard.Children[0] as DoubleAnimation;
            DoubleAnimation y = storyboard.Children[1] as DoubleAnimation;
            x.From = 0;
            y.From = 0;
        }

        private string JoystickState()
        {
            double deltaX = destX - centerAnchor.X;
            double deltaY = destY - centerAnchor.Y;
            double limit = Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
            if (this.ellipseRadius * norma > limit)
            {
                return "Move";
            }
            else
            {
                return "Anchor";
            }
        }

        private void Movement()
        {
            if (JoystickState().Equals("Move"))
            {
                Storyboard storyboard = Knob.Resources["CenterKnob"] as Storyboard;
                DoubleAnimation x = storyboard.Children[0] as DoubleAnimation;
                DoubleAnimation y = storyboard.Children[1] as DoubleAnimation;
                x.To = destX - centerAnchor.X;
                y.To = destY - centerAnchor.Y;
                storyboard.Begin();
                x.From = x.To;
                y.From = y.To;
            }
            if (JoystickState().Equals("Anchor"))
            {
                Dock();
            }
            Normalize();
        }

        private void Dock()
        {
            isClicked = false;
            destX = centerAnchor.X;
            destY = centerAnchor.Y;
            Movement();
            //Normalize();
        }

        private void Normalize()
        {
            double tmpX = destX - centerAnchor.X;
            double tmpY = destY - centerAnchor.Y;
            normalX = tmpX / ellipseRadius;
            normalY = tmpY / ellipseRadius;
            if (MoveEvent != null)
            {
                MoveEvent(normalX, -normalY);
            }
        }
        private void centerKnob_Completed(object sender, EventArgs e) { }

        private void Frame_MouseLeave(object sender, MouseEventArgs e)
        {
            Dock();
        }

        // if i want to add it again -> put MouseLeave="General_MouseLeave" in the xaml line 7
        //private void General_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    Dock();
        //}


        private void Knob_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isClicked = true;
        }
        private void Base_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Dock();
        }

        private void Base_MouseMove(object sender, MouseEventArgs e)
        {
            if (isClicked == true)
            {
                destX = e.GetPosition(Base).X;
                destY = e.GetPosition(Base).Y;
                Movement();
            }
        }
    }
}
