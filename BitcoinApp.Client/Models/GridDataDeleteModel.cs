
namespace BitcoinApp.Client.Models
{
    public class GridDataDeleteModel<T> where T : class
    {
        public List<T> DeletedRows { get; set; } = null!;
    }
}