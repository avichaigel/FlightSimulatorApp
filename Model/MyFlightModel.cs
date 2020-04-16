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
        private static Mutex mutex = new Mutex();

        //constuctor
        public MyFlightModel(ITelnetClient telnetClient)
        {
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
            mutex.WaitOne();
            var task = Task.Run(() => telnetClient.Write(message));
            if (!task.Wait(TimeSpan.FromSeconds(10)))
            {
                mutex.ReleaseMutex();
                throw new Exception("Server not responding for 10 seconds");
            }
            mutex.ReleaseMutex();
        }

        public string Read()
        {
            mutex.WaitOne();
            var task = Task.Run(() => telnetClient.Read());
            if (task.Wait(TimeSpan.FromSeconds(10)))
            {
                mutex.ReleaseMutex();
                return task.Result;
            }
            else
            {
                mutex.ReleaseMutex();
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
                    mutex.WaitOne();
                    telnetClient.Write("get /position/latitude-deg");
                    string tempStr = telnetClient.Read();
                    Latitude = Double.Parse(tempStr);
                    this.Write("get /position/longitude-deg");
                    tempStr = telnetClient.Read();
                    Longtitude = Double.Parse(tempStr);
                    telnetClient.Write("get /instrumentation/airspeed-indicator/indicated-speed-kt");
                    tempStr = telnetClient.Read();
                    Air_Speed = Double.Parse(tempStr);
                    this.Write("get /instrumentation/gps/indicated-altitude-ft");
                    tempStr = telnetClient.Read();
                    Altitude = Double.Parse(tempStr);
                    this.Write("get /instrumentation/attitude-indicator/internal-roll-deg");
                    tempStr = telnetClient.Read();
                    Roll = Double.Parse(tempStr);
                    this.Write("get /instrumentation/attitude-indicator/internal-pitch-deg");
                    tempStr = telnetClient.Read();
                    Pitch = Double.Parse(tempStr);
                    this.Write("get /instrumentation/altimeter/indicated-altitude-ft");
                    tempStr = telnetClient.Read();
                    Altimeter = Double.Parse(tempStr);
                    this.Write("get /instrumentation/heading-indicator/indicated-heading-deg");
                    tempStr = telnetClient.Read();
                    Heading = Double.Parse(tempStr);
                    this.Write("get /instrumentation/gps/indicated-ground-speed-kt");
                    tempStr = telnetClient.Read();
                    Ground_Speed = Double.Parse(tempStr);
                    this.Write("get /instrumentation/gps/indicated-vertical-speed");
                    tempStr = telnetClient.Read();
                    Vertical_Speed = Double.Parse(tempStr);
                    Location = latitude.ToString() + "," +   longtitude.ToString();
                    mutex.ReleaseMutex();
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
            //Location = latitude + "," + longtitude;
        }

        public void StartWriting(string command)
        {
            this.telnetClient.Write(command);
            this.telnetClient.Read();
        }

        /*public void UpdateThrottle(string command)
        {
            if (!stop)
            {
                this.telnetClient.Write(command);
                this.telnetClient.Read();
            }
            
        }

        public void UpdateAileron(string command)
        {
            if (!stop)
            {
                this.telnetClient.Write(command);
                this.telnetClient.Read();
            }
        }

        public void UpdateRudder(string command)
        {
            if (!stop)
            {
                this.telnetClient.Write(command);
                this.telnetClient.Read();
            }
        }

        public void UpdateElevator(string command)
        {
            if (!stop)
            {
                this.telnetClient.Write(command);
                this.telnetClient.Read();
            }
        }*/




    }
}
