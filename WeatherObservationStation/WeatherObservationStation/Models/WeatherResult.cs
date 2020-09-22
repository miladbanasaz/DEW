using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherObservationStation.Models
{
    public class WeatherResult
    {
        public List<WeatherItem> Observations { get; set; }
    }

    public class WeatherItem
    {
        public int Wmo { get; set; }
        public string Station { get; set; }
        public List<WeatherData> Data { get; set; }
    }
}