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
        private bool telnetError;
        public void Connect(string ip, int port)
        {
            //ip = "1.0.0.127";
            //port = 5402;
            client = new TcpClient();
            IsConnected = true;
            client.Connect(ip, port);
            telnetError = false;
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
            //NetworkStream myNetworkStream = client.GetStream();
            //if (myNetworkStream.CanRead)
            //{
            //    byte[] bufferReader = new byte[1024];
            //    StringBuilder wholeMessage = new StringBuilder();
            //    int readBytesNum = 0;
            //    while (myNetworkStream.DataAvailable)
            //    {
            //        readBytesNum = myNetworkStream.Read(bufferReader, 0, bufferReader.Length);
            //        wholeMessage.AppendFormat("{0}", Encoding.ASCII.GetString(bufferReader, 0, readBytesNum));
            //    }
            //    // print the message to the console
            //    Console.WriteLine("Your Message: " + wholeMessage);
            //    return wholeMessage.ToString();
            //}
            //return null;
            byte[] buffer = new byte[1024];
            client.GetStream().Read(buffer, 0, 1024);
            string data = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
            return data;
        }

        public void Write(string command)
        {
            string official_command = command + "\n";
            byte[] read = Encoding.ASCII.GetBytes(official_command);
            client.GetStream().Write(read, 0, read.Length);
        }
    }
}
