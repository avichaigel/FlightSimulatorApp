﻿using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public MainViewModel MainVM { get; internal set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            IFlightModel flightModel = new MyFlightModel(new MyTelnetClient());
            MainVM = new MainViewModel(flightModel);
        }

    }
}
