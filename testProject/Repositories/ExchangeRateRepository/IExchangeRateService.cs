namespace testProject.Repositories.ExchangeRateRepository
{
    public interface IExchangeRateService
    {
        Task<Dictionary<string, decimal>> GetExchangeRates(string baseCurrency, List<string> targetCurrencies);
    }
}
