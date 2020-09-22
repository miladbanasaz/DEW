using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAverageCalculator.Models
{
   public class BureauWeather
    {
        public Observation Observations { get; set; }
        
    }


    public class Observation
    {
        [JsonProperty("data")]
        public List<BureauWeatherData> WeatherData { get; set; }
    }
    public class BureauWeatherData
    {
        [JsonProperty("air_temp")]
        public decimal Temperature { get; set; }

        [JsonProperty("local_date_time_full")]
        public string DateOrg { get; set; }

        //2020 09 20 20 30 00
        public DateTime ParsedDate => DateTime.ParseExact(DateOrg, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
    }
}
