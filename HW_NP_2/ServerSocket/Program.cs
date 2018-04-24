using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSocket
{
    [Serializable]
    public class Region
    {
        public string City { get; set; }
        public string Index { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 4444);
            List<Region> regions = null;
            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            string str;
            using (FileStream fs = new FileStream("index.json", FileMode.Open))
            {
                byte[] buf = new byte[fs.Length];
                fs.Read(buf, 0, buf.Length);
                str = Encoding.UTF8.GetString(buf);
                regions = JsonConvert.DeserializeObject<List<Region>>(str);
            }
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);

                while (true)
                {
                    Console.WriteLine("Ожидаем соединение через порт {0}", ipEndPoint);

                    Socket handler = sListener.Accept();
                    string data = null;

                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    var reply = regions
                        .Where(x => x.City.ToLower().Equals(data.ToLower()))
                        .FirstOrDefault();
                    if (reply != null)
                    {
                        var replyMsg = reply.Index;
                        byte[] msg = Encoding.UTF8.GetBytes(replyMsg);
                        handler.Send(msg);
                        if (data.IndexOf("<TheEnd>") > -1)
                        {
                            Console.WriteLine("Сервер завершил соединение с клиентом.");
                            break;
                        }
                    }
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
