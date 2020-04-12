using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulatorApp.Model;

namespace FlightSimulatorApp.ViewModel
{
    class SlidersViewModel : INotifyPropertyChanged
    {
        private IFlightModel model;
        public SlidersViewModel(IFlightModel model)
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

        //Properties that change those in Model according to View
        public double VM_throttle
        {
            set => model.Throttle = value;
        }
        public double VM_aileron
        {
            set => model.Aileron = value;
        }
    }
}