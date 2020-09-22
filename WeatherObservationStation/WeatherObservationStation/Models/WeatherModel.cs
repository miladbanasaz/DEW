using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherObservationStation.Models
{
   public class WeatherModel
    {
        public Observation Observations { get; set; }
        
    }
    
    public class Observation
    {
        [JsonProperty("data")]
        public List<WeatherData> WeatherData { get; set; }
    }
    public class WeatherData
    {
        [JsonProperty("air_temp")]
        public decimal Temperature { get; set; }

        [JsonProperty("local_date_time_full")]
        public string DateOrg { get; set; }

        //2020 09 20 20 30 00
        public DateTime ParsedDate => DateTime.ParseExact(DateOrg, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

        [JsonProperty("sort_order")]
        public int SortOrder { get; set; }

        [JsonProperty("wmo")]
        public int WMO { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("history_product")]
        public string HistoryProduct { get; set; }

        [JsonProperty("local_date_time")]
        public string LocalDateTime { get; set; }

        [JsonProperty("aifstime_utc")]
        public string AifstimeUtc { get; set; }

        [JsonProperty("lat")]
        public decimal Lat { get; set; }
        [JsonProperty("lon")]
        public decimal Lon { get; set; }

        [JsonProperty("apparent_t")]
        public decimal ApparentTemp { get; set; }

        [JsonProperty("cloud")]
        public string Cloud { get; set; }
        [JsonProperty("cloud_base_m")]
        public string CloudBaseM { get; set; }
        [JsonProperty("cloud_oktas")]
        public string CloudOktas { get; set; }
        [JsonProperty("cloud_type_id")]
        public string CloudTypeId { get; set; }
        [JsonProperty("cloud_type")]
        public string CloudType { get; set; }
        [JsonProperty("delta_t")]
        public string DeltaT { get; set; }
        [JsonProperty("gust_kmh")]
        public string GustKmh { get; set; }
        [JsonProperty("gust_kt")]
        public string GustKt { get; set; }

        [JsonProperty("dewpt")]
        public string Dewpt { get; set; }

        [JsonProperty("press")]
        public string Press { get; set; }
        [JsonProperty("press_qnh")]
        public string PressQnh { get; set; }

        [JsonProperty("press_msl")]
        public string PressMsl { get; set; }
        [JsonProperty("press_tend")]
        public string PressTend { get; set; }
        [JsonProperty("rain_trace")]
        public string RainTrace { get; set; }

        [JsonProperty("rel_hum")]
        public string RelHum { get; set; }

        [JsonProperty("sea_state")]
        public string SeaState { get; set; }

        [JsonProperty("swell_dir_worded")]
        public string SwellDirWorded { get; set; }

        [JsonProperty("swell_height")]
        public string SwellHeight { get; set; }

        [JsonProperty("swell_period")]
        public string SwellPeriod { get; set; }

        [JsonProperty("vis_km")]
        public string VisKm { get; set; }

        [JsonProperty("weather")]
        public string WeatherType { get; set; }

        [JsonProperty("wind_dir")]
        public string WindDir { get; set; }

        [JsonProperty("wind_spd_kmh")]
        public string WindSpdKmh { get; set; }

        [JsonProperty("wind_spd_kt")]
        public string WindSpdKt { get; set; }		
				
				
        

    }

    

}
