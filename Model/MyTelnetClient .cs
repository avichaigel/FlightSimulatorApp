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
            NetworkStream myNetworkStream = client.GetStream();
            if (myNetworkStream.CanRead)
            {
                byte[] bufferReader = new byte[1024];
                StringBuilder wholeMessage = new StringBuilder();
                int readBytesNum = 0;
                while (myNetworkStream.DataAvailable)
                {
                    readBytesNum = myNetworkStream.Read(bufferReader, 0, bufferReader.Length);
                    wholeMessage.AppendFormat("{0}", Encoding.ASCII.GetString(bufferReader, 0, readBytesNum));
                }
                // print the message to the console
                Console.WriteLine("Your Message: " + wholeMessage);
                return wholeMessage.ToString();
            }
            return null;
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
