using BitcoinApp.Server.Models;

namespace BitcoinApp.Server.Services
{
    public interface IRetrievedValuesService
    {
        void AddRetrievedValue(DateTime retrievedAt, decimal bitcoinValue);
        void MarkAsSaved(DateTime retrievedAt);
        IEnumerable<BitcoinValueRetrieved> GetRetrievedValues();
    }
}