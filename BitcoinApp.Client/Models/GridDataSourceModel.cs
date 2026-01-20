
namespace BitcoinApp.Client.Models
{
    public class GridDataSourceModel<T> : GridDataResultModel
    {
        public GridDataSourceDataModel<T> Data { get; set; } = null!;
    }
}