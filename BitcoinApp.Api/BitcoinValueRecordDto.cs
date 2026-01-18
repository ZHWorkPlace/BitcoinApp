
namespace BitcoinApp.Api
{
    public class BitcoinValueRecordDto
    {
        public DateTime RetrievedAt { get; set; }
        public decimal BitcoinValue { get; set; }
        public string Note { get; set; } = null!;
    }
}