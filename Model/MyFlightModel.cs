using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    public class MyFlightModel : IFlightModel
    {
        ITelnetClient telnetClient;
        volatile Boolean stop;
        public event PropertyChangedEventHandler PropertyChanged;
        private static Mutex mutex = new Mutex();
        private string errorMsg;

        private double throttle;
        private double aileron;
        private double elevator;
        private double rudder;
        private double latitude;
        private double longtitude;
        private double air_Speed;
        private double altitude;
        private double roll;
        private double pitch;
        private double altimeter;
        private double heading;
        private double ground_Speed;
        private double vertical_Speed;
        private string location;

        //constuctor
        public MyFlightModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            InitializeDashboard();
        }
        //Properties
        public string Error
        {
            get => errorMsg;
            set
            {
                errorMsg = value;
                NotifyPropertyChanged("Error");
            }
        }
        public double Throttle
        {
            get => throttle;
            set
            {
                throttle = value;
                telnetClient.Write("set /controls/engines/current-engine/throttle " + value);
            }
        }
        public double Aileron
        {
            get => aileron;
            set
            {
                aileron = value;
                telnetClient.Write("set /controls/flight/aileron " + value);
            }
        }
        public double Elevator
        {
            get => elevator;
            set
            {
                elevator = value;
                telnetClient.Write("set /controls/flight/elevator " + value);
            }
        }
        public double Rudder
        {
            get => rudder;
            set
            {
                rudder = value;
                telnetClient.Write("set /controls/flight/rudder " + value);
            }
        }
        public double Latitude {
            get => latitude;
            set
            {
                latitude = value;
                NotifyPropertyChanged("Latitude");
            }
        }
        public double Longtitude {
            get => longtitude; 
            set {
                longtitude = value;
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

        public string Location
        {
            get => location;
            set
            {
                location = value;
                NotifyPropertyChanged("Location");
            }
        }

        //methods
        public void Connect(string ip, int port)
        {
            try
            {
                telnetClient.Connect(ip, port);
            }
            catch (Exception)
            {
                Error = "Could not connect to server";
            }
        }

        public void Disconnect()
        {
            stop = true;
            telnetClient.Disconnect();
        }

        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public string Read(double currentValue)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var read = telnetClient.Read();
            if (sw.ElapsedMilliseconds > 10000)
            {
                Error = "Server not responding for 10 seconds";
                return currentValue.ToString();
                //TODO check if "return read" is good, or maybe something else should be returned
            }
            return read;
        }

        //get values for properties from simulator
        public void Start()
        {
            new Thread(delegate ()
            {
                while(!stop)
                {
                    mutex.WaitOne();
                    try
                    {
                        telnetClient.Write("get /position/latitude-deg");
                        Latitude = Double.Parse(this.Read(latitude));
                        telnetClient.Write("get /position/longitude-deg");
                        Longtitude = Double.Parse(this.Read(longtitude));
                        telnetClient.Write("get /instrumentation/airspeed-indicator/indicated-speed-kt");
                        Air_Speed = Double.Parse(this.Read(air_Speed));
                        telnetClient.Write("get /instrumentation/gps/indicated-altitude-ft");
                        Altitude = Double.Parse(this.Read(altitude));
                        telnetClient.Write("get /instrumentation/attitude-indicator/internal-roll-deg");
                        Roll = Double.Parse(this.Read(roll));
                        telnetClient.Write("get /instrumentation/attitude-indicator/internal-pitch-deg");
                        Pitch = Double.Parse(this.Read(pitch));
                        telnetClient.Write("get /instrumentation/altimeter/indicated-altitude-ft");
                        Altimeter = Double.Parse(this.Read(altimeter));
                        telnetClient.Write("get /instrumentation/heading-indicator/indicated-heading-deg");
                        Heading = Double.Parse(this.Read(heading));
                        telnetClient.Write("get /instrumentation/gps/indicated-ground-speed-kt");
                        Ground_Speed = Double.Parse(this.Read(ground_Speed));
                        telnetClient.Write("get /instrumentation/gps/indicated-vertical-speed");
                        Vertical_Speed = Double.Parse(this.Read(vertical_Speed));
                        Location = latitude.ToString() + "," + longtitude.ToString();

                        Thread.Sleep(250);
                    }
                    catch (Exception)
                    {
                        Error = "Connection with server is lost";
                    }
                    mutex.ReleaseMutex();
                }
            }).Start();
        }

        void IFlightModel.NotifyPropertyChanged(string propName)
        {
            throw new NotImplementedException();
        }

        public void InitializeDashboard()
        {
            // Initialize all the properties to 0.s.
            Air_Speed = 0;
            Altitude = 0;
            Roll = 0;
            Pitch = 0;
            Altimeter = 0;
            Heading = 0;
            Ground_Speed = 0;
            Vertical_Speed = 0;
            // Reading map values from the simulator.
            Latitude = 0;
            Longtitude = 0;
            //Location = latitude + "," + longtitude;
        }
    }
}
