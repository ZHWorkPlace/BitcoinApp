
namespace BitcoinApp.Server
{
    public class BitcoinAppServerConfiguration
    {
        public string ExchangeRateApiEndpoint { get; set; } = string.Empty;
        public string BitcoinPriceApiEndpoint { get; set; } = string.Empty;
        public int BitcoinWorkerIntervalSeconds { get; set; }
    }
}