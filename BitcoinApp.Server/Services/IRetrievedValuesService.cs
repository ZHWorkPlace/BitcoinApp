using BitcoinApp.Server.Models;

namespace BitcoinApp.Server.Services
{
    public interface IRetrievedValuesService
    {
        void AddRetrievedValue(BitcoinValueRetrieved value);
        void MarkAsSaved(DateTime retrievedAt);
        IEnumerable<BitcoinValueRetrieved> GetRetrievedValues();
    }
}