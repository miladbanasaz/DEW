using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Globalization;
using WeatherAverageCalculator.Models;
using Newtonsoft.Json;

namespace WeatherAverageCalculator
{
    public class WeatherService
    {
        private readonly string _url;
        public WeatherService(string url)
        {
            _url = url;
        }

        public BureauWeather GetWeather()
        {
            var wh = GetDetails(_url);
            var res = JsonConvert.DeserializeObject<BureauWeather>(wh);
            return res;
        }

        #region private methods

        private string GetDetails(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.UserAgent = "Milad/1.0.0";
            req.Accept = "*/*";
            var resp = (HttpWebResponse)req.GetResponse();

            var result = string.Empty;
            using (var sr = new StreamReader(resp.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                sr.Close();
            }

            return result;
        }
        #endregion
    }
}
