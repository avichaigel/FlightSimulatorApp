using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private string headingAddress, verticalSpeedAddress, groundSpeedAddress, airSpeedAddress, altitudeAddress, rollAddress, pitchAddress,
            altimeterAddress, latitudeAddress, longitudeAddress;
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

        //constuctor
        public MyFlightModel(ITelnetClient telnetClient)
        {
            initializeAddresses();
            this.telnetClient = telnetClient;
            initializeDashboard();
        }
        //Properties
        public double Throttle
        {
            get => throttle;
            set
            {
                throttle = value;
                this.Write("set /controls/engines/current-engine/throttle " + value + "\n");
            }
        }
        public double Aileron
        {
            get => aileron;
            set
            {
                aileron = value;
                this.Write("set /controls/flight/aileron " + value + "\n");
            }
        }
        public double Elevator
        {
            get => elevator;
            set
            {
                elevator = value;
                this.Write("set /controls/flight/elevator " + value + "\n");
            }
        }
        public double Rudder
        {
            get => rudder;
            set
            {
                rudder = value;
                this.Write("set /controls/flight/rudder " + value + "\n");
            }
        }
        public double Latitude {
            get => latitude;
            set
            {
                latitude = value;
                NotifyPropertyChanged("Longtitude");
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

        //methods
        public void connect(string ip, int port)
        {
            telnetClient.Connect(ip, port);
        }

        public void disconnect()
        {
            telnetClient.Disconnect();
        }

        private void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void Write(string message)
        {
            var task = Task.Run(() => telnetClient.Write(message));
            if (!task.Wait(TimeSpan.FromSeconds(10)))
            {
            throw new Exception("Server not responding for 10 seconds");
            }
        }

        public string Read()
        {
            var task = Task.Run(() => telnetClient.Read());
            if (task.Wait(TimeSpan.FromSeconds(10)))
            {
                return task.Result;
            }
            else
            {
                throw new Exception("Server not responding for 10 seconds");
            }
        }

        //get values for properties from simulator
        public void start()
        {
            new Thread(delegate ()
            {
                while(!stop)
                {
                    //this.Write("get /position/latitude-deg");
                    //Latitude = Double.Parse(this.Read());
                    //this.Write("get /position/longitude-deg");
                    //Longtitude = Double.Parse(this.Read());
                    //this.Write("get /instrumentation/airspeed-indicator/indicated-speed-kt");
                    telnetClient.Write("get /instrumentation/airspeed-indicator/indicated-speed-kt");
                    string tempStr = telnetClient.Read();
                    Air_Speed = Double.Parse(tempStr);
                    //this.Write("get /instrumentation/gps/indicated-altitude-ft");
                    //Altitude = Double.Parse(this.Read());
                    //this.Write("get /instrumentation/attitude-indicator/internal-roll-deg");
                    //Roll = Double.Parse(this.Read());
                    //this.Write("get /instrumentation/attitude-indicator/internal-pitch-deg");
                    //Pitch = Double.Parse(this.Read());
                    //this.Write("get /instrumentation/altimeter/indicated-altitude-ft");
                    //Altimeter = Double.Parse(this.Read());
                    //this.Write("get /instrumentation/heading-indicator/indicated-heading-deg");
                    //Heading = Double.Parse(this.Read());
                    //this.Write("get /instrumentation/gps/indicated-ground-speed-kt");
                    //Ground_Speed = Double.Parse(this.Read());
                    //this.Write("get /instrumentation/gps/indicated-vertical-speed");
                    //Vertical_Speed = Double.Parse(this.Read());

                    Thread.Sleep(250);
                }               
            }).Start();
        }

        void IFlightModel.NotifyPropertyChanged(string propName)
        {
            throw new NotImplementedException();
        }

        public void initializeDashboard()
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
            //Location = latitude + "," + longitude;
        }

        private void initializeAddresses()
        {
            airSpeedAddress = "/instrumentation/airspeed-indicator/indicated-speed-kt";
            altitudeAddress = "/instrumentation/gps/indicated-altitude-ft";
            rollAddress = "/instrumentation/attitude-indicator/internal-roll-deg";
            pitchAddress = "/instrumentation/attitude-indicator/internal-pitch-deg";
            altimeterAddress = "/instrumentation/altimeter/indicated-altitude-ft";
            headingAddress = "/instrumentation/heading-indicator/indicated-heading-deg";
            groundSpeedAddress = "/instrumentation/gps/indicated-ground-speed-kt";
            verticalSpeedAddress = "/instrumentation/gps/indicated-vertical-speed";
            // Reading map values from the simulator.
            latitudeAddress = "/position/latitude-deg";
            longitudeAddress = "/position/longitude-deg";
        }
    }
}
