using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Send_btn_Click(object sender, EventArgs e)
        {
            AsyncClient.StartClient();
        }

        public class AsyncClient
        {
            private const int port = 3080;
 
            private static ManualResetEvent connectDone = new ManualResetEvent(false);
            private static ManualResetEvent sendDone = new ManualResetEvent(false);
            private static ManualResetEvent receiveDone = new ManualResetEvent(false);
  
            private static String response = String.Empty;

            public static void StartClient()
            {
                try
                {
                    IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                    IPAddress ip = host.AddressList[0];
                    IPEndPoint remote_IpEp = new IPEndPoint(ip, port);
                    
                    Socket clientSocket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    clientSocket.BeginConnect(remote_IpEp, new AsyncCallback(ConnectCallback), clientSocket);
                    connectDone.WaitOne(); 
                    Send(clientSocket, "Тест <eof>");
                    sendDone.WaitOne();
                    Receive(clientSocket);
                    receiveDone.WaitOne();
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            private static void ConnectCallback(IAsyncResult ar)
            {
                try
                {
                    Socket clientSocket = ar.AsyncState as Socket;
                    clientSocket.EndConnect(ar);
                    connectDone.Set();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            private static void Receive(Socket clientSocket)
            {
                try
                {
                    State state = new State();
                    state.socket = clientSocket;
                    clientSocket.BeginReceive(state.buffer, 0, State.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            private static void ReceiveCallback(IAsyncResult ar)
            {
                try
                {
                    State state = ar.AsyncState as State;
                    Socket clientSocket = state.socket;
                    int bytesRead = clientSocket.EndReceive(ar);
                    if (bytesRead > 0)
                    {
                        state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                        clientSocket.BeginReceive(state.buffer, 0, State.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                    }
                    else
                    {
                        if (state.sb.Length > 1)
                        {
                            response = state.sb.ToString();
                        }
                        receiveDone.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            private static void Send(Socket clientSocket, String data)
            {
                byte[] bytesData = Encoding.ASCII.GetBytes(data);
                clientSocket.BeginSend(bytesData, 0, bytesData.Length, 0, new AsyncCallback(SendCallback), clientSocket);
            }

            private static void SendCallback(IAsyncResult ar)
            {
                try
                {
                    Socket clientSocket = (Socket)ar.AsyncState;
                    int bytesSent = clientSocket.EndSend(ar);
                    sendDone.Set();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        
        public class State
        {
            public Socket socket = null;
            public const int BufferSize = 256;
            public byte[] buffer = new byte[BufferSize];
            public StringBuilder sb = new StringBuilder();
        }
    }
}
