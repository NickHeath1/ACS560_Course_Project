using System;
using System.Collections.Generic;
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
            byte[] json = File.ReadAllBytes("TCPSessionTest.json");
            
            NetworkStream stream = client.GetStream();
            stream.Write(json, 0, json.Length);
            if (stream.DataAvailable)
            {
                byte[] data = new byte[4096];
                stream.Read(data, 0, data.Length);
                string dataString = UnicodeEncoding.ASCII.GetString(data);
                Console.WriteLine(dataString);
            }
        }
    }
}
