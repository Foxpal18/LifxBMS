using System;
using System.IO;
using Microsoft.VisualBasic;

namespace Lif_x_BMS
{
    public class auth
    {
        public string grantType { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string tenant { get; set; }

        public static void createKeyStore()
        {
            var dirPath = Path.Combine(Environment.CurrentDirectory, @"\Lifx_BMS\");
            var filePath = Path.Combine(dirPath, "l.txt");

            if (!File.Exists(filePath))
            {
                string defaultResponse = "";
                string apiInput = Interaction.InputBox("Please enter your LIFX API Key.", "Lifx_BMS", defaultResponse);

                Directory.CreateDirectory(dirPath);
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(apiInput);
                }
            }
        }
    }
}
