using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Console;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Введите сообщение...");
            string message = ReadLine();
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ip = host.AddressList[0];
            IPEndPoint ipEP = new IPEndPoint(ip, 4455);
            Socket socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipEP);
            byte[] buff = new byte[1024];
            byte[] msg = Encoding.UTF8.GetBytes(message);
            int send = socket.Send(msg);
            int receive = socket.Receive(buff);
            string result = Encoding.ASCII.GetString(buff);
            WriteLine(result);
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            ReadLine();
        }
    }
}
