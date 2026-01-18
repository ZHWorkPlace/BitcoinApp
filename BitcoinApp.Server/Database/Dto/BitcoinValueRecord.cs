
namespace BitcoinApp.Server.Database.Dto
{
    public class BitcoinValueRecord
    {
        public DateTime RetrievedAt { get; set; }
        public decimal BitcoinValue { get; set; }
        public string Note { get; set; } = null!;
    }
}