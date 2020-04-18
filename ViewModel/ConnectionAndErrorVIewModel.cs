using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulatorApp.Model;

namespace FlightSimulatorApp.ViewModel
{
    public class ConnectionAndErrorVIewModel : INotifyPropertyChanged
    {
        private IFlightModel model;
        private string ip, port;
        private bool errorWindowEmptyFlag = true;
        public ConnectionAndErrorVIewModel(IFlightModel model)
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

        public string VM_IP
        {
            set
            {
                this.ip = value;
            }
        }
        public string VM_Port
        {
            set
            {
                this.port = value;
            }
        }

        
        public string VM_Status
        {
            get
            {
                return model.Status;
            }
        }
        
        
        /*
        public string VM_Err
        {
            set
            {
                flightSimulatorModel.clearError();
                errorWindowEmptyFlag = true;
            }
            get
            {
                errorWindowEmptyFlag = false;
                return flightSimulatorModel.Err;
            }
        }*/

        public bool VM_isErrorWindowEmpty
        {
            get
            {
                if (this.model.Error != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }

        public void VM_Connect()
        {
            model.Connect(ip, int.Parse(port));
        }

        public void VM_Disconnect()
        {
            model.Disconnect();
        }

    }


}
