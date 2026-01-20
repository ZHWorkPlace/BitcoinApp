using BitcoinApp.Server.Models;

namespace BitcoinApp.Server.Services
{
    public interface IRetrievedValuesService
    {
        void AddRetrievedValue(DateTime retrievedAt, decimal valueEur, decimal valueCzk, decimal exchangeRate);
        void MarkAsSaved(DateTime retrievedAt);
        IEnumerable<BitcoinValueRetrieved> GetRetrievedValues();
    }
}