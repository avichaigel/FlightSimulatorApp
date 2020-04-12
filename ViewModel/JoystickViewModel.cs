using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulatorApp.Model;

namespace FlightSimulatorApp.ViewModel
{
    class JoystickViewModel : INotifyPropertyChanged
    {
        private IFlightModel model;
        public JoystickViewModel(IFlightModel model)
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

        public double VM_elevator
        {
            set => model.Elevator = value;
        }
        public double VM_rudder
        {
            set => model.Rudder = value;
        }
    }
}