using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPTester
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient("localhost", 2346);
            NetworkStream stream = client.GetStream();
            TCPSignal signal = new TCPSignal
            {
                SignalType = 1,
                NewSession = new Session
                {
                    HostPlayer = "Meg"
                }
            };
            string json = JsonConvert.SerializeObject(signal).Replace("\r", "") + '\r';
            byte[] jsonBytes = ASCIIEncoding.ASCII.GetBytes(json);
            stream.Write(jsonBytes, 0, jsonBytes.Length);
            string data = File.ReadAllText("TCPSessionTest.json").Replace("\r", "") + '\r';
            byte[] dataBytes = ASCIIEncoding.ASCII.GetBytes(data);
            while (true)
            {
                byte[] received = new byte[8192];
                stream.Write(dataBytes, 0, data.Length);
                stream.Read(received, 0, received.Length);
                TCPSignal messageSignal = (TCPSignal)JsonConvert.DeserializeObject(ASCIIEncoding.ASCII.GetString(received).Replace("\0", ""), typeof(TCPSignal));
                Console.WriteLine(messageSignal.PlayerMessage);
            }
        }
    }
}
