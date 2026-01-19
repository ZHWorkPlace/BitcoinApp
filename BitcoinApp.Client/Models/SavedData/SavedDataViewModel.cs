
namespace BitcoinApp.Client.Models.SavedData
{
    public class SavedDataViewModel
    {
        public bool Result { get; set; }
        public List<SavedDataRecordViewModel> Data { get; set; } = null!;
    }
}