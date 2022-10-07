using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Lif_x_BMS
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            auth.createKeyStore();
            StreamReader sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, @"\Lifx_BMS\l.txt"));
            string key = sr.ReadLine();
            while (true)
            {
                Log("Getting BMS Token.");
                int result = await BMSConnector.InitializeBMSClient();
                if (result == 1)
                {
                    Err("Retrying in 10 seconds.");
                    Thread.Sleep(10000);
                    continue;
                }
                while (true)
                {
                    var status = await BMSConnector.TicketAlert(key);
                    if (status == 1) // if unsuccessful, generate new BMS client
                    {
                        Thread.Sleep(10000);
                        break;
                    }
                    Thread.Sleep(60000);
                }
            }
        }
        public static void Log(string logMessage)
        {
            Console.WriteLine($"[{DateTime.Now}] {logMessage}");
        }
        public static void Err(string logMessage)
        {
            Console.WriteLine($"[{DateTime.Now}][Error] {logMessage}");
        }
    }
}
