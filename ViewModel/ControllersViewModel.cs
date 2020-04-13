using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulatorApp.Model;

namespace FlightSimulatorApp.ViewModel
{
    class ControllersViewModel
    {
        private IFlightModel model;
        public ControllersViewModel(IFlightModel model)
        {
            this.model = model;
        }

        public double VM_Elevator
        {
            set => model.Elevator = value;
        }
        public double VM_Rudder
        {
            set => model.Rudder = value;
        }
        public double VM_Throttle
        {
            set => model.Throttle = value;
        }
        public double VM_Aileron
        {
            set => model.Aileron = value;
        }
    }
}