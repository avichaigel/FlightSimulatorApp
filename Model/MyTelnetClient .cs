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
        private bool isConnected = false;
        private static Mutex mutex = new Mutex();

        public bool IsConnected { get => isConnected; set => isConnected = value; }

        public void Connect(string ip, int port)
        {
            client = new TcpClient();
            isConnected = true;
            client.Connect(ip, port);
        }

        public void Disconnect()
        {
            if (isConnected)
            {
                isConnected = false;
                client.Close();
            }
        }

        public string Read()
        {
            mutex.WaitOne();
            byte[] buffer = new byte[1024];
            client.GetStream().Read(buffer, 0, 1024);
            string data = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
            mutex.ReleaseMutex();
            return data;
        }

        public void Write(string command)
        {
            if (isConnected)
            {
                mutex.WaitOne();
                string official_command = command + "\n";
                byte[] read = Encoding.ASCII.GetBytes(official_command);
                client.GetStream().Write(read, 0, read.Length);
                mutex.ReleaseMutex();
            }
        }
    }
}
