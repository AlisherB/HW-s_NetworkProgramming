using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using static System.Console;

namespace Client
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Send_btn_Click(object sender, EventArgs e)
        {
            Connect("127.0.0.1", msg_textBox.Text);
        }

        private void Connect(string server, string text)
        {
            try
            {
                int port = 3080;
                TcpClient client = new TcpClient(server, port);
                byte[] data = Encoding.ASCII.GetBytes(text);
                
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                WriteLine("Send: {0}", text);

                data = new byte[256];
                var response = string.Empty;
                var bytes = stream.Read(data, 0, data.Length);
                response = Encoding.ASCII.GetString(data, 0, bytes);
                WriteLine("Received: {0}", response);
                client.Close();
            }
            catch (Exception ex)
            {
                WriteLine(ex.ToString());
            }
            ReadLine();
        }
    }
}
