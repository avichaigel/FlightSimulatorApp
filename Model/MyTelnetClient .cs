using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;

namespace FlightSimulatorApp.Model
{
    public class MyTelnetClient : ITelnetClient
    {
        private TcpClient client;
        private bool IsConnected = false;
        private static Mutex mutex = new Mutex();

        public void Connect(string ip, int port)
        {
            //ip = "1.0.0.127";
            //port = 5402;
            client = new TcpClient();
            IsConnected = true;
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

        public string Read()
        {
            byte[] buffer = new byte[1024];
            client.GetStream().Read(buffer, 0, 1024);
            string data = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
            return data;
        }

        public void Write(string command)
        {
            if (IsConnected)
            {
                string official_command = command + "\n";
                byte[] read = Encoding.ASCII.GetBytes(official_command);
                client.GetStream().Write(read, 0, read.Length);
            }
        } 
    }
}
