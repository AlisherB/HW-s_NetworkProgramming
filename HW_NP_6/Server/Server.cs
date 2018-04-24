using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Client_240418;
using static System.Console;

namespace Server
{
    public class Server
    {
        private TcpListener server;
        public Server(String host, int port)
        {
            this.server = new TcpListener(IPAddress.Parse(host), port);
        }

        public delegate void OnDisconnectDelegate(Client cli);

        public event OnDisconnectDelegate OnDisconnect;

        public void Start()
        {
            server.Start();
            var syncObj = new ManualResetEvent(false);
            while (true)
            {
                syncObj.Set();
                server.BeginAcceptSocket((IAsyncResult ar) => 
                {
                    var client = new Client();
                    client.Clients.Add(client);
                    client.Socket = ((Socket)ar.AsyncState).EndAccept(ar);
                    syncObj.Reset();
                    WriteLine("Connection accepted:" + client.ToString());
                    client.Socket.BeginReceive(client.Buffer, 0, client.Buffer.Length, SocketFlags.None, EndReceive, client);
                }, server.Server);
            }
        }

        public void EndReceive(IAsyncResult ar1)
        {
            var client = (Client)ar1.AsyncState;
            var readed = client.Socket.EndReceive(ar1);
            //var cli = (Client)ar1.AsyncState;
            if (readed == 0)
            {
                OnDisconnect.Invoke(client);
            }
            else
            {
                // read bytes
                //messsage:asdlkasjdklasjd/r/n
                new MemoryStream().Read(client.Buffer, 0, client.Buffer.Length);
                new MemoryStream().Write(client.Buffer, 0, readed);

                client.Socket.BeginReceive(client.Buffer, 0, client.Buffer.Length, SocketFlags.None, EndReceive, client);
            }
        }

        public void SendClient(Client client, byte[] data) => 
            client.Socket.BeginSend(client.Buffer, 0, client.Buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), client);

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
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
}
