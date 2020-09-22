using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using WeatherObservationStation.Tools;
using System.Net.Http;
using WeatherObservationStation.Models;
using System.Threading.Tasks;

namespace WeatherObservationStation
{
    public class WeatherService
    {
        private const string serviceUrl = "http://www.bom.gov.au/fwo/IDS60901/IDS60901.{0}.json";

        private Dictionary<int, string> stations = new Dictionary<int, string>()
        {
            {94672,"Adelaide Airport" },
            {95676,"Edinburgh" },
            {94677,"Hindmarsh Island" },
            {94683,"Kuitpo" }
        };

        public async Task<WeatherResult> GetWeathers()
        {
            var res = new WeatherResult()
            {
                Observations = new List<WeatherItem>()
            };

            foreach (var item in stations)
            {
                var wh = await GetWeather(item.Key);
                res.Observations.Add(wh);
            }
            return res;
        }

        public async Task<WeatherItem> GetWeather(int wmo)
        {
            var station = stations[wmo];

            var wh = await Globals.RequestUrl<WeatherModel>(
                string.Format(serviceUrl, wmo),
                HttpMethod.Get,
                headers:new Dictionary<string, string>()
                {
                    { "User-Agent","Milad/1.0.0" },
                    {"Accept","*/*" }
                });

            var res = new WeatherItem()
            {
                Wmo = wmo,
                Station = station,
                Data = wh.Observations.WeatherData
            };

            return res;
        }

    }
}
