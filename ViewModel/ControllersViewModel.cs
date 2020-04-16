using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulatorApp.Model;

namespace FlightSimulatorApp.ViewModel
{
    public class ControllersViewModel : INotifyPropertyChanged
    {
        private IFlightModel model;
        public ControllersViewModel(IFlightModel model)
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

        public double VM_Throttle
        {
            set
            {
                if (Math.Abs(value - model.Throttle) > 0.1)
                {
                    string stringThrottle = "set /controls/engines/current-engine/throttle ";
                    if (value > 1)
                    {
                        value = 1;
                    }
                    else if (value < 0)
                    {
                        value = 0;
                    }
                    stringThrottle += value.ToString();
                    //model.UpdateThrottle(stringThrottle);
                    model.StartWriting(stringThrottle);
                }
            }
        }

        public double VM_Rudder
        {
            set
            {
                if (Math.Abs(value - model.Rudder) > 0.1)
                {
                    string stringRudder = "set /controls/flight/rudder ";
                    stringRudder += value.ToString();
                    //model.UpdateRudder(stringRudder);
                    model.StartWriting(stringRudder);
                }

            }
        }

        public double VM_Aileron
        {
            set
            {
                if (Math.Abs(value - model.Aileron) > 0.1)
                {
                    string stringAileron = "set /controls/flight/aileron ";
                    if (value > 1)
                    {
                        value = 1;
                    }
                    else if (value < -1)
                    {
                        value = -1;
                    }

                    stringAileron += value.ToString();
                    //model.UpdateAileron(stringAileron);
                    model.StartWriting(stringAileron);
                }
            }
        }

        public double VM_Elevator
        {
            set
            {
                if (Math.Abs(value - model.Elevator) > 0.1)
                {
                    string stringElevator = "set /controls/flight/elevator ";
                    if (value > 1)
                    {
                        value = 1;
                    }
                    else if (value < -1)
                    {
                        value = -1;
                    }
                    stringElevator += value.ToString();
                    //model.UpdateElevator(stringElevator);
                    model.StartWriting(stringElevator);
                }

            }
        }

        /*public double VM_Elevator
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
        }*/
    }
}