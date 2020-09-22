using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace WorkingWithCSVFile
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = new CsvProvider("https://apps.waterconnect.sa.gov.au/file.csv");
            Console.WriteLine("Downloading csv...");
            var items = provider.DownloadCsv();

            Console.WriteLine("Download completed");

            Console.WriteLine("Saving csv...");
            provider.SaveCsv($"{Directory.GetCurrentDirectory()}\\UpdateFile.csv", items);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

    }
}
