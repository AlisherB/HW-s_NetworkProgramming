using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Console;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ip = host.AddressList[0];
            IPEndPoint ep = new IPEndPoint(ip, 3080);
            Socket listener = new Socket(SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(ep);
            listener.Listen(10);
            Socket socket = listener.Accept();
            byte[] buff = new byte[1024];
            socket.Receive(buff);
            string result = Encoding.UTF8.GetString(buff, 0, buff.Length);
            WriteLine(result);
            byte[] msg = Encoding.UTF8.GetBytes(result);
            socket.Send(msg);
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            ReadLine();
        }
    }
}
