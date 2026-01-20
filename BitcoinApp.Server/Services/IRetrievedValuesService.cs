using BitcoinApp.Server.Models;

namespace BitcoinApp.Server.Services
{
    public interface IRetrievedValuesService
    {
        public ExchangeRate ExchangeRate { get; }
        IEnumerable<BitcoinValueRetrieved> GetRetrievedValues();

        void AddRetrievedValue(Guid id, DateTime retrievedAt, decimal valueEur, decimal valueCzk, decimal exchangeRate);
    }
}