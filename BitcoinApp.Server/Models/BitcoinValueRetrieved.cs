
namespace BitcoinApp.Server.Models
{
    public class BitcoinValueRetrieved
    {
        public Guid Id { get; set; }
        public bool IsSaved { get; set; }
        public DateTime RetrievedAt { get; set; }
        public decimal ValueEur { get; set; }
        public decimal ValueCzk { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}