using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using testProject.Models;
using testProject.Models.HomeModels;
using testProject.Repositories.ExchangeRateRepository;
using testProject.Repositories.TimeRepository;
using testProject.Repositories.WeatherRepository;
using testProject.ViewModels.HomeModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using testProject.ViewModels.AuthenticationViewModels;

namespace testProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherService weatherService;
        private readonly IExchangeRateService exchangeService;
        private readonly ITimeService timeService;

        public HomeController(ILogger<HomeController> logger, IWeatherService weatherService, IExchangeRateService exchangeService, 
            ITimeService timeService)
        {
            _logger = logger;
            this.weatherService = weatherService;
            this.exchangeService = exchangeService;
            this.timeService = timeService;
        }

        public async Task<IActionResult> Index()
        {
            DateTime today = DateTime.Today;
            var cities = new List<string> { "Istanbul", "Kigali", "Abidjan" };
            var weatherDataList = new List<WeatherData>();
            var timeDataList = new List<TimeData>();
            var currencies = new List<string> { "TRY", "EUR", "RWF", "XOF" };
            var baseCurrency = "USD";
            var exchangeData = new ExchangeRateData()
            {
                Base = baseCurrency,
                Date = today.ToShortDateString(),
                Rates = await exchangeService.GetExchangeRates(baseCurrency, currencies)
            };

            foreach (var city in cities)
            {
                var weatherData = await weatherService.GetWeather(city);
                weatherDataList.Add(weatherData);

                var timeData = await timeService.GetTime(city);
                timeDataList.Add(timeData);
            }
            var viewModel = new HomeViewModel { WeatherData = weatherDataList, ExchangeRateData=exchangeData, TimeData=timeDataList};

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Access");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
