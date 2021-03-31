using InternshipClass.Utilities;
using InternshipClass.WebAPI;
using InternshipClass.WebAPI.Controllers;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using Xunit;

namespace InternshipClass.Tests
{
    public class DayTests
    {
        [Fact]
        public void CheckEpochConversion()
        {
            // Assume
            long ticks = 1617184800;

            // Act
            DateTime dateTime = DateTimeConverter.ConvertEpochToDateTime(ticks);


            // Assert
            Assert.Equal(31, dateTime.Day);
            Assert.Equal(03, dateTime.Month);
            Assert.Equal(2021, dateTime.Year);

        }

        [Fact]
        public void ConvertOutputOfWeatherAPIToWeatherForecast()
        {
            // Assume
            //https://api.openweathermap.org/data/2.5/onecall?lat=45.75&lon=25.3333&exclude=hourly,minutely&appid=5c22adf85237e02133761e817996de14
            var lat = 45.75;
            var lon = 25.3333;
            var apiKey = "5c22adf85237e02133761e817996de14";
            Microsoft.Extensions.Logging.ILogger<WeatherForecastController> nullLogger = new NullLogger<WeatherForecastController>();
            var weatherForecastController = new WeatherForecastController(nullLogger);

            // Act
            var weatherForecasts = weatherForecastController.FetchWeatherForecasts(lat, lon, apiKey);
            WeatherForecast weatherForecastForTomorrow = weatherForecasts[1];

            // Assert
            Assert.Equal(285.39, weatherForecastForTomorrow.TemperatureK);
        }


    }
}
