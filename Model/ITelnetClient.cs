using System;
using System.Collections.Generic;
using System.Text;

namespace FlightSimulatorApp.Model
{
	public interface ITelnetClient
	{
		bool IsConnected { get; set; }

		void Connect(string ip, int port);
		void Disconnect();
		void Write(string command);
		string Read();
	}
}
