using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WeatherObservationStation.Models;

namespace WeatherObservationStation.Controllers
{
    public class WeatherController : ApiController
    {
        // GET api/Weather
        public Task<WeatherResult> Get()
        {
            return new WeatherService().GetWeathers();
        }

        // GET api/Weather/94672
        public Task<WeatherItem> Get(int id)
        {
            return new WeatherService().GetWeather(id);
        }

        
    }
}
