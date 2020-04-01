using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    class MyFlightModel : IFlightModel
    {
        ITelnetClient telnetClient;
        public event PropertyChangedEventHandler PropertyChanged;
        volatile Boolean stop;

        private double latitude;
        private double longitude;
        private double air_Speed;
        private double altitude;
        private double roll;
        private double pitch;
        private double altimeter;
        private double heading;
        private double ground_Speed;
        private double vertical_Speed;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        //Properties
        public double Latitude { 
            get => latitude;
            set
            {
                latitude = value;
                NotifyPropertyChanged("Latitude");
            }
        }
        public double Longitude {
            get => longitude; 
            set {
                longitude = value;
                NotifyPropertyChanged("Longtitude");
            }
        }
        public double Air_Speed { 
            get => air_Speed;
            set {
                air_Speed = value;
                NotifyPropertyChanged("Air_Speed");
            }
        }
        public double Altitude { 
            get => altitude;
            set {
                altitude = value;
                NotifyPropertyChanged("Altitude");
            }
        }
        public double Roll {
            get => roll;
            set {
                roll = value;
                NotifyPropertyChanged("Roll");
            }
        }
        public double Pitch { 
            get => pitch;
            set {
                pitch = value;
                NotifyPropertyChanged("Pitch");
            }
        }
        public double Altimeter { 
            get => altimeter;
            set { 
                altimeter = value;
                NotifyPropertyChanged("Altimeter");
            }
        }
        public double Heading { 
            get => heading;
            set { 
                heading = value;
                NotifyPropertyChanged("Heading");
            }
}
        public double Ground_Speed { 
            get => ground_Speed;
            set {
                ground_Speed = value;
                NotifyPropertyChanged("Ground_Speed");
            }
        }
        public double Vertical_Speed { 
            get => vertical_Speed;
            set {
                vertical_Speed = value;
                NotifyPropertyChanged("Vertical_Speed");
            }
        }

        //constuctor
        public MyFlightModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
        }

        public void connect(string ip, int port)
        {
            telnetClient.Connect(ip, port);
        }

        public void disconnect()
        {
            telnetClient.Disconnect();
        }

        public void move(double speed, int angle)
        {
            //TODO: maybe set throttle, aileron, elevator and rudder rceieved from VM
            
        }

        //get values for properties from simulator
        public void start()
        {
            new Thread(delegate ()
            {
                while(!stop)
                {
                    telnetClient.Write("get latitude");
                    Latitude = Double.Parse(telnetClient.Read());
                    telnetClient.Write("get longtitude");
                    Longitude = Double.Parse(telnetClient.Read());
                    telnetClient.Write("get air_speed");
                    Air_Speed = Double.Parse(telnetClient.Read());
                    telnetClient.Write("get altitude");
                    Altitude = Double.Parse(telnetClient.Read());
                    telnetClient.Write("get roll");
                    Roll = Double.Parse(telnetClient.Read());
                    telnetClient.Write("get pitch");
                    Pitch = Double.Parse(telnetClient.Read());
                    telnetClient.Write("get altimeter");
                    Altimeter = Double.Parse(telnetClient.Read());
                    telnetClient.Write("get heading");
                    Heading = Double.Parse(telnetClient.Read());
                    telnetClient.Write("get ground_speed");
                    Ground_Speed = Double.Parse(telnetClient.Read());
                    telnetClient.Write("get vertical_speed");
                    Vertical_Speed = Double.Parse(telnetClient.Read());

                    Thread.Sleep(250);
                }               
            }).Start();
        }
    }
}
