using BitcoinApp.Server.Models;
using System.Collections.Concurrent;

namespace BitcoinApp.Server.Services
{
    public class RetrievedValuesService : IRetrievedValuesService
    {
        public ExchangeRate ExchangeRate { get; } = new ExchangeRate();
        private ConcurrentBag<BitcoinValueRetrieved> RetrievedValues { get; } = [];

        public void AddRetrievedValue(DateTime retrievedAt, decimal valueEur, decimal valueCzk, decimal exchangeRate)
        {
            AddRetrievedValue(new BitcoinValueRetrieved
            {
                RetrievedAt = retrievedAt,
                ValueEur = valueEur,
                ValueCzk = valueCzk,
                ExchangeRate = exchangeRate,
                IsSaved = false
            });
        }

        private void AddRetrievedValue(BitcoinValueRetrieved value)
        {
            RetrievedValues.Add(value);
        }

        public void MarkAsSaved(DateTime retrievedAt)
        {
            RetrievedValues.Single(v => v.RetrievedAt == retrievedAt).IsSaved = true;
        }

        public IEnumerable<BitcoinValueRetrieved> GetRetrievedValues()
        {
            return RetrievedValues;
        }
    }
}