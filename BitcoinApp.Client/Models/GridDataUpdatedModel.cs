namespace BitcoinApp.Client.Models
{
    public class GridDataUpdatedModel<T>
    {
        public List<T> UpdatedRows { get; set; } = null!;
    }
}