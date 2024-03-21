using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using testProject.Models.HomeModels;

namespace testProject.Repositories.WeatherRepository
{
    public class WeatherService : IWeatherService
    {
        private readonly string apiKey;

        public WeatherService(string apiKey)
        {
            this.apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        }

        public async Task<WeatherData> GetWeather(string city)
        {
            var apiUrl = $"http://api.weatherstack.com/current?access_key={apiKey}&query={city}";
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(apiUrl);

                if (!string.IsNullOrEmpty(response))
                {
                    var json = JObject.Parse(response);

                    var weatherData = new WeatherData
                    {
                        City = GetValue(json, "location", "name"),
                        Temperature = GetValue(json, "current", "temperature"),
                        WindSpeed = GetValue(json, "current", "wind_speed"),
                        Precipitation = GetValue(json, "current", "precip"),
                        Humidity = GetValue(json, "current", "humidity"),
                        CloudCover = GetValue(json, "current", "cloudcover"),
                        SunriseTime = GetValue(json, "current", "sunrise"),
                        SunsetTime = GetValue(json, "current", "sunset"),
                        WeatherDescription = GetValue(json, "current", "weather_descriptions")
                    };

                    return weatherData;
                }
                else
                {
                    throw new ApplicationException("Unable to retrieve weather data.");
                }
            }
        }

        private static string GetValue(JObject json, params string[] propertyNames)
        {
            JToken token = json;

            foreach (var propertyName in propertyNames)
            {
                token = token?[propertyName];

                if (token == null)
                {
                    return null; // Property not found
                }
            }

            return token.ToString();
        }
    }

}