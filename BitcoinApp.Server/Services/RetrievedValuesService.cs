using BitcoinApp.Server.Models;
using System.Collections.Concurrent;

namespace BitcoinApp.Server.Services
{
    public class RetrievedValuesService : IRetrievedValuesService
    {
        private ConcurrentBag<BitcoinValueRetrieved> retrievedValues { get; } = [];

        public void AddRetrievedValue(BitcoinValueRetrieved value)
        {
            retrievedValues.Add(value);
        }

        public void MarkAsSaved(DateTime retrievedAt)
        {
            retrievedValues.Single(v => v.RetrievedAt == retrievedAt).IsSaved = true;
        }

        public IEnumerable<BitcoinValueRetrieved> GetRetrievedValues()
        {
            return retrievedValues;
        }
    }
}