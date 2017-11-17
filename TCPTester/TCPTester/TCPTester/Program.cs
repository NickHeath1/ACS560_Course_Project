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
            while (true)
            {
                byte[] data = UnicodeEncoding.ASCII.GetBytes("Hello!\r");
                stream.Write(data, 0, data.Length);
            }
        }
    }
}
