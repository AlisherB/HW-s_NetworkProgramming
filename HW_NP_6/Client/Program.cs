using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Client_240418
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Client
    {
        private byte[] buffer = new byte[1024];

        public byte[] Buffer { get => buffer; set => buffer = value; }

        public Guid Id { get; } = Guid.NewGuid();

        public List<Client> Clients { get; set; }

        public Socket Socket { get; set; }// internal set; }
        

        public override string ToString()
        {
            return String.Format($"{this.Socket.RemoteEndPoint.ToString()}:{this.Id}");
        }
    }
}
