using System.IO;
using System.Net;
using System.Threading.Tasks;
using static System.Console;

namespace Http
{
    class Program
    {
        static void Main(string[] args)
        {
            RequestAsync();
            ReadLine();
        }

        public static async Task RequestAsync()
        {
            int size = 1024;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://wallscloud.net/wallpaper/3d/soti/6Vqz/1366x768/download");
            request.AddRange(size);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    WriteLine(sr.ReadToEnd());
                }
            }
            response.Close();
        }
    }
}
