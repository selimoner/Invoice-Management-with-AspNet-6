namespace testProject.Models.HomeModels
{
    public class ExchangeRateData
    {
        public string Base { get; set; }
        public string Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
        public ExchangeRateData()
        {
            Rates = new Dictionary<string, decimal>();
        }
    }
}
