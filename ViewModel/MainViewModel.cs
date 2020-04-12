using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private IFlightModel flightModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public DashboardViewModel DashBoardVM { get; set; }
        public MapViewModel MapVM { get; set; }
        public ControllersViewModel ControllersVM { get; set; }

        public MainViewModel(IFlightModel flightModel)
        {
            this.flightModel = flightModel;
            DashBoardVM = new DashboardViewModel(flightModel);
            MapVM = new MapViewModel(flightModel);
            ControllersVM = new ControllersViewModel(flightModel);
        }
    }
}
