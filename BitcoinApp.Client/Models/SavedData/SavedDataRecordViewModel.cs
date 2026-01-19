
namespace BitcoinApp.Client.Models.SavedData
{
    public class SavedDataRecordViewModel
    {
        public DateTime RetrievedAt { get; set; }
        public decimal BitcoinValue { get; set; }
        public string Note { get; set; } = null!;
    }
}