using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModel
{
    class MainViewModel
    {
        public DashboardViewModel DashBoardVM { get; set; }
        public MapViewModel MapVM { get; set; }
        public JoystickViewModel JoystickVM { get; set; }
        public SlidersViewModel SlidersVM { get; set; }
        public MainViewModel(IFlightModel flightModel)
        {
            flightModel = flightModel;
            DashBoardVM = new DashboardViewModel(flightModel);
            MapVM = new MapViewModel(flightModel);

        }
    }
}
