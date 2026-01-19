
namespace BitcoinApp.Client.Models
{
    public class GridDataSourceModel<T>
    {
        public bool Result { get; set; }
        public GridDataSourceDataModel<T> Data { get; set; } = null!;
    }
}