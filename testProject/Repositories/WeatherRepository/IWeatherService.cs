using testProject.Models.HomeModels;

namespace testProject.Repositories.WeatherRepository
{
    public interface IWeatherService
    {
        Task<WeatherData> GetWeather(string city);
    }
}
