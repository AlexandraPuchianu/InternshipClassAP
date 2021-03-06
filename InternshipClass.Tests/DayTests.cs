using InternshipClass.Utilities;
using InternshipClass.WebAPI;
using InternshipClass.WebAPI.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using SR = System.IO.StreamReader;
using Xunit;

namespace InternshipClass.Tests
{
    public class DayTest
    {
        private IConfigurationRoot configuration;

        public DayTest()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

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

            WeatherForecastController weatherForecastController = InstantiateWeatherForecastController();

            // Act
            var weatherForecasts = weatherForecastController.Get();

            // Assert
            Assert.Equal(5, weatherForecasts.Count);
        }


        [Fact]
        public void ConvertWeatherJsonToWeatherForecast()
        {

            // Assume
            string content = GetStreamLines("weatherForecast");
            WeatherForecastController weatherForecastController = InstantiateWeatherForecastController();

            // Act
            var weatherForecasts = weatherForecastController.ConvertResponseContentToWeatherForecastList(content);
            WeatherForecast weatherForecastForTomorrow = weatherForecasts[1];

            // Assert
            Assert.Equal(285.39, weatherForecastForTomorrow.TemperatureK);
        }


        [Fact]
        public void ShouldHandleJsonErrorFromOpenWeatherAPI()
        {
            // Assume
            string content = GetStreamLines("weatherForecast_Exception");
            WeatherForecastController weatherForecastController = InstantiateWeatherForecastController();

            // Act
            
            // Assert
            Assert.Throws<Exception>(() => weatherForecastController.ConvertResponseContentToWeatherForecastList(content));

        }
        private string GetStreamLines(string resourceName)
        {
            var assembly = this.GetType().Assembly;

            using var stream = assembly.GetManifestResourceStream($"InternshipClass.Tests.{resourceName}.json");

            SR streamReader = new SR(stream);
            var streamReaderLines = "";
            while (!streamReader.EndOfStream) 
            {
                streamReaderLines += streamReader.ReadLine();
            }

            return streamReaderLines;
            
        }
        
        private WeatherForecastController InstantiateWeatherForecastController()
        {
            Microsoft.Extensions.Logging.ILogger<WeatherForecastController> nullLogger = new NullLogger<WeatherForecastController>();
            var weatherForecastController = new WeatherForecastController(nullLogger, configuration);
            return weatherForecastController;
        }


    }
}
