
namespace BitcoinApp.Server.Api
{
    public class BitcoinValueRecordDto
    {
        public long Id { get; set; }
        public DateTime RetrievedAt { get; set; }
        public decimal BitcoinValue { get; set; }
        public string Note { get; set; } = null!;
    }
}