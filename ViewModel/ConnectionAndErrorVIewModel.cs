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
        
        
        
        public string VM_ErrorClear
        {
            set
            {
                this.model.Error = null;
            }
            //get
            //{
            //    errorWindowEmptyFlag = false;
            //    return flightSimulatorModel.Err;
            //}
        }

        public bool VM_HasError
        {
            get
            {
                if (this.model.Error != null)
                {
                    return true;
                }
                else
                {
                    return false;
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
