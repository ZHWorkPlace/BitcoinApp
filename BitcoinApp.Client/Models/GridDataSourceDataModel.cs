
namespace BitcoinApp.Client.Models
{
    public class GridDataSourceDataModel<T>
    {
        public List<T> Contents { get; set; } = null!;

        public GridDataSourcePaginationModel Pagination { get; set; } = null!;
    }
}