using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulatorApp.Model;

namespace FlightSimulatorApp.ViewModel
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private IFlightModel model;
        public DashboardViewModel(IFlightModel model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public double VM_Air_Speed
        {
            get => model.Air_Speed;
        }
        public double VM_altitude
        {
            get => model.Altitude;
        }
        public double VM_roll
        {
            get => model.Roll;
        }
        public double VM_pitch
        {
            get => model.Pitch;
        }
        public double VM_altimeter
        {
            get => model.Altimeter;
        }
        public double VM_heading
        {
            get => model.Heading;
        }
        public double VM_ground_Speed
        {
            get => model.Ground_Speed;
        }
        public double VM_vertical_Speed
        {
            get => model.Vertical_Speed;
        }
    }
}
