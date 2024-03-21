using testProject.Models.HomeModels;
using testProject.ViewModels.AuthenticationViewModels;

namespace testProject.ViewModels.HomeModels
{
    public class HomeViewModel
    {
        public List<WeatherData> WeatherData { get; set; }
        public ExchangeRateData ExchangeRateData { get; set; }
        public List<TimeData> TimeData { get; set; }

    }
}
