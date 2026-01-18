
namespace BitcoinApp.Server.Models
{
    public class BitcoinValueRetrieved
    {
        public bool IsSaved { get; set; }
        public DateTime RetrievedAt { get; set; }
        public decimal Value { get; set; }
    }
}