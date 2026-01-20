
namespace BitcoinApp.Client.Models.SavedData
{
    public class SavedDataRecordViewModel
    {
        public DateTime RetrievedAt { get; set; }
        public decimal ValueEur { get; set; }
        public decimal ValueCzk { get; set; }
        public decimal ExchangeRate { get; set; }
        public string Note { get; set; } = null!;
    }
}