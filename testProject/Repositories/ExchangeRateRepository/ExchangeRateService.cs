using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json; 
using testProject.Models.HomeModels;

namespace testProject.Repositories.ExchangeRateRepository
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly string apiKey;

        public ExchangeRateService(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<Dictionary<string, decimal>> GetExchangeRates(string baseCurrency, List<string> targetCurrencies)
        {
            using (var httpClient = new HttpClient())
            {
                var apiUrl = $"http://api.exchangeratesapi.io/v1/latest?access_key={apiKey}&format=1";
                var exchangeRatesForCurrencies = new Dictionary<string, decimal>();

                try
                {
                    var response = await httpClient.GetStringAsync(apiUrl);

                    if (!string.IsNullOrEmpty(response))
                    {
                        using (JsonDocument doc = JsonDocument.Parse(response))
                        {
                            if (doc.RootElement.TryGetProperty("rates", out JsonElement ratesElement))
                            {
                                foreach (var targetCurrency in targetCurrencies)
                                {
                                    if (ratesElement.TryGetProperty(targetCurrency, out JsonElement rateElement))
                                    {
                                        if (rateElement.TryGetDecimal(out decimal rate))
                                        {
                                            exchangeRatesForCurrencies[targetCurrency] = rate;
                                        }
                                    }
                                }
                            }
                        }

                        return exchangeRatesForCurrencies;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                return exchangeRatesForCurrencies;
            }
        }

    }
}