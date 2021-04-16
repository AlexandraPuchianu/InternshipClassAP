using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using InternshipClass.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace InternshipClass.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly double latitude;
        private readonly double longitude;
        private readonly string apiKey;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;

            this.latitude = double.Parse(configuration["WeatherForecast:Latitude"], CultureInfo.InvariantCulture);
            this.longitude = double.Parse(configuration["WeatherForecast:Longitude"], CultureInfo.InvariantCulture);
            this.apiKey = configuration["WeatherForecast:ApiKey"];
        }

        /// <summary>
        /// Getting Weather Forecast for five days for default location.
        /// </summary>
        /// <returns>List of WeatherForecast objects. </returns>
        [HttpGet]
        public List<WeatherForecast> Get()
        {
            var weatherForecasts = Get(this.latitude, this.longitude);

            return weatherForecasts.GetRange(1, 5);
        }

        /// <summary>
        /// Getting Weather Forecast for today and for seven days ahead for specific location.
        /// </summary>
        /// <param name="latitude">It should be between -90 and 90. For example: latitude for Brasov is 45.75.</param>
        /// <param name="longitude">It should be between -180 and 180. For example: longitude for Brasov is 25.3333.</param>
        /// <returns>List of WeatherForecast objects.</returns>
        [HttpGet("/forecast")]
        public List<WeatherForecast> Get(double latitude, double longitude)
        {
            var client = new RestClient($"https://api.openweathermap.org/data/2.5/onecall?lat={latitude}&lon={longitude}&exclude=hourly,minutely&appid={apiKey}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return ConvertResponseContentToWeatherForecastList(response.Content);
        }

        [NonAction]
        public List<WeatherForecast> ConvertResponseContentToWeatherForecastList(string content)
        {
            // TODO: content sometimes is empty string
            if (content == string.Empty)
            {
                Console.WriteLine("Exception: The Weather Forecast content is empty");
                return new List<WeatherForecast>();
            }
            else
            {
            JToken root = JObject.Parse(content);
            JToken testToken = root["daily"];
            if (testToken == null)
            {
                    JToken codToken = root["cod"];
                    JToken messageToken = root["message"];

                    throw new Exception($"Weather API doesn't work. Please check the Weather API : {messageToken}({codToken})");
            }
            List<WeatherForecast> forecasts = new List<WeatherForecast>();
            foreach (var token in testToken)
            {
                var forecast = new WeatherForecast();
                forecast.Date = DateTimeConverter.ConvertEpochToDateTime(long.Parse(token["dt"].ToString()));
                forecast.TemperatureK = double.Parse(token["temp"]["day"].ToString());
                forecast.Summary = token["weather"][0]["description"].ToString();

                forecasts.Add(forecast);
            }

            return forecasts;
            }
        }
    }
}
