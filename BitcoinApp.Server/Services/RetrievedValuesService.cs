using BitcoinApp.Server.Models;
using System.Collections.Concurrent;

namespace BitcoinApp.Server.Services
{
    public class RetrievedValuesService
    {
        private ConcurrentBag<BitcoinValueRetrieved> retrievedValues { get; } = [];

        public void AddRetrievedValue(BitcoinValueRetrieved value)
        {
            retrievedValues.Add(value);
        }

        public List<BitcoinValueRetrieved> GetRetrievedValues()
        {
            return retrievedValues.ToList();
        }
    }
}