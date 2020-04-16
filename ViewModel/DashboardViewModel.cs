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
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public string VM_Error
        {
            get => model.Error;
        }
        public double VM_Air_Speed
        {
            get => model.Air_Speed;
        }
        public double VM_Altitude
        {
            get => model.Altitude;
        }
        public double VM_Roll
        {
            get => model.Roll;
        }
        public double VM_Pitch
        {
            get => model.Pitch;
        }
        public double VM_Altimeter
        {
            get => model.Altimeter;
        }
        public double VM_Heading
        {
            get => model.Heading;
        }
        public double VM_Ground_Speed
        {
            get => model.Ground_Speed;
        }
        public double VM_Vertical_Speed
        {
            get => model.Vertical_Speed;
        }
    }
}
