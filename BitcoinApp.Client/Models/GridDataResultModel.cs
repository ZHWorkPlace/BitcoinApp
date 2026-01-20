
namespace BitcoinApp.Client.Models
{
    public class GridDataResultModel
    {
        public GridDataResultModel()
        {
            Result = true;
        }
        public GridDataResultModel(string message)
        {
            Result = false;
            Message = message;
        }

        public bool Result { get; set; }
        public string? Message { get; set; }
    }
}