﻿using System;
using System.Collections.Generic;
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
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        /// <summary>
        /// Getting Weather Forecast for five days.
        /// </summary>
        /// <returns>Enumerable of WeatherForecast objects. </returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var weatherForecasts = FetchWeatherForecasts();

            return weatherForecasts.GetRange(1, 5);
        }

        public List<WeatherForecast> FetchWeatherForecasts()
        {
            var lat = double.Parse(configuration["WeatherForecast:Latitude"]);
            var lon = double.Parse(configuration["WeatherForecast:Longitude"]);
            var apiKey = configuration["WeatherForecast:ApiKey"];

            var client = new RestClient($"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&exclude=hourly,minutely&appid={apiKey}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return ConvertResponseContentToWeatherForecastList(response.Content);
        }

        public List<WeatherForecast> ConvertResponseContentToWeatherForecastList(string content)
        {
            JToken root = JObject.Parse(content);
            JToken testToken = root["daily"];
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
