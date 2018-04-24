using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ChatServerLogic.Start();
            ReadLine();
        }
    }

    public class ChatServerLogic
    {
        private static ManualResetEvent socketEvent  = new ManualResetEvent(false);

        public static void Start()
        {
            byte[] bytes = new byte[1024];
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ip = host.AddressList[0];
            IPEndPoint ipEP = new IPEndPoint(ip, 3080);
            Socket listener = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(ipEP);
                listener.Listen(100);
                while (true)
                {
                    socketEvent .Reset();
                    WriteLine("Waiting for a connection...");
                    listener.BeginAccept(new AsyncCallback(BeginAcceptCallback), listener); 
                    socketEvent .WaitOne();
                }
            }
            catch (Exception e)
            {
                WriteLine(e.ToString());
            }
            ReadLine();
        }

        public static void BeginAcceptCallback(IAsyncResult ar)
        {
            socketEvent.Set();
            Socket listener = ar.AsyncState as Socket;
            var handler = listener.EndAccept(ar);
            State state = new State();
            state.socket = handler;
            handler.BeginReceive(state.buff, 0, State.bufferSize, 0, new AsyncCallback(ReadCallback), state);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            var content = string.Empty;
            State state = (State)ar.AsyncState;
            var handler = state.socket;
            int bytesRead = handler.EndReceive(ar);
            if (bytesRead > 0)
            {
                state.sb.Append(Encoding.ASCII.GetString(state.buff, 0, bytesRead));
                content = state.sb.ToString();
                if (content.IndexOf("<eof>") > -1)
                {
                    WriteLine("Read {0} bytes from socket. \n Data : {1}", content.Length, content);
                    Send(handler, content);
                }
                else
                    handler.BeginReceive(state.buff, 0, State.bufferSize, 0, new AsyncCallback(ReadCallback), state);
            }
        }

        private static void Send(Socket handler, String data)
        { 
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = ar.AsyncState as Socket;
                int bytesSent = handler.EndSend(ar);
                WriteLine("Sent {0} bytes to client.", bytesSent);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                WriteLine(e.ToString());
            }
        }
    }
    
    public class State
    {
        public Socket socket = null;
        public const int bufferSize = 1024;
        public byte[] buff = new byte[bufferSize];
        public StringBuilder sb = new StringBuilder();
    }
}
