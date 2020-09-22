using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAverageCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var whService = new WeatherService("http://www.bom.gov.au/fwo/IDS60901/IDS60901.94672.json");
            var wheathers = whService.GetWeather();

            var tempAvg = Math.Round(wheathers.Observations.WeatherData
                .Where(d => d.ParsedDate >= DateTime.Now.AddDays(-3).Date)
                .Average(d => d.Temperature), 2);
            Console.WriteLine($"The average weather for last three days is: {tempAvg}");
            Console.WriteLine("Press any key to exit ...");
            Console.ReadKey();
        }
    }
}
