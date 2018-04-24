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

namespace ClientSocket
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Find_button_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(SendMessage);
            th.Start();
        }

        private void SendMessage()
        {
            byte[] bytes = new byte[1024];

            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 3080);
            Socket socket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipEndPoint);
            int bytesRec = 0;
            int bytesSent;
            
            if (!string.IsNullOrEmpty(city_textBox.Text))
            {
                string message = city_textBox.Text;
                byte[] msg = Encoding.UTF8.GetBytes(message);
                bytesSent = socket.Send(msg);
                
                try
                {
                    bytesRec = socket.Receive(bytes);
                }
                catch (Exception ex) { }
                if (bytesRec > 0)
                {
                    index_textBox.Invoke(new Action(() => { index_textBox.Text = String.Format("{0}", Encoding.UTF8.GetString(bytes, 0, bytesRec));}));
                }
            }
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
