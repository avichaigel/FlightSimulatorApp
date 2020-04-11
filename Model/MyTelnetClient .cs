using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace FlightSimulatorApp.Model
{
    class MyTelnetClient : ITelnetClient
    {
        private TcpClient client;
        private bool IsConnected = false;
        public void Connect(string ip, int port)
        {
            //ip = "1.0.0.127";
            //port = 5402;
            client = new TcpClient();
            client.Connect(ip, port);
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                IsConnected = false;
                client.Close();
            }
        }

        public string Read(string command)
        {
            string official_command = "get " + command + "\n";
            byte[] read = Encoding.ASCII.GetBytes(official_command);
            client.GetStream().Write(read, 0, read.Length);
            byte[] buffer = new byte[64];
            client.GetStream().Read(buffer, 0, 64);
            string data = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
            return data;
        }

        public void Write(string command)
        {
            string official_command = command + "\n";
            byte[] read = Encoding.ASCII.GetBytes(official_command);
            client.GetStream().Write(read, 0, read.Length);
            byte[] buffer = new byte[64];
            client.GetStream().Read(buffer, 0, 64);
            string data = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
            Console.WriteLine(data);
        }
    }
}
