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
        private static Mutex mutex = new Mutex();
        public void Connect(string ip, int port)
        {
            //client = new TcpClient();
            //IsConnected = true;
            //try
            //{
            //    client.Connect(ip, port);
            //    telnetError = false;
            //}
            //catch (Exception)
            //{
            //    telnetError = true;
            //    //(Application.Current as App).model.Err = "Couldn't connect to server";
            //    Console.WriteLine("Couldn't connect to server");
            //}
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
            mutex.WaitOne();
            byte[] buffer = new byte[1024];
            client.GetStream().Read(buffer, 0, 1024);
            string data = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
            Console.WriteLine(data);
            mutex.ReleaseMutex();
            return data;

        }


        public void Write(string command)
        {
            if (IsConnected)
            {
                try
                {
                    mutex.WaitOne();
                    string official_command = command + "\n";
                    byte[] read = Encoding.ASCII.GetBytes(official_command);
                    client.GetStream().Write(read, 0, read.Length);
                    mutex.ReleaseMutex();
                }
                catch (Exception)
                {
                    telnetError = true;
                    mutex.ReleaseMutex();
                    Disconnect();
                    if (!telnetError)
                    {
                        //(Application.Current as App).model.Err = "Server Communication is done";
                        Console.WriteLine("Server Communication is done");
                    }
                }
            }
        }

        public bool getTelnetError()
        {
            return this.telnetError;
        }
    }
}
