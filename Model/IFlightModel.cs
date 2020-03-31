using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    interface IFlightModel
    {
        //connection to the simulator
        void connect(string ip, int port);
        void disconnect();
        void start();

        void move(double speed, int angle);
    }
}
