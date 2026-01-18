
namespace BitcoinApp.Server.Api
{
    public class BitcoinValueRetrievedDto
    {
        public DateTime RetrievedAt { get; set; }
        public decimal Value { get; set; }
    }
}