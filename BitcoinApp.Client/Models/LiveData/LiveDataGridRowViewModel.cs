using System.Text.Json.Serialization;

namespace BitcoinApp.Client.Models.LiveData
{
    public class LiveDataGridRowViewModel
    {
        public Guid Id { get; set; }
        public bool IsSaveEnabled { get; set; }
        public DateTime RetrievedAt { get; set; }
        public decimal ValueEur { get; set; }
        public decimal ValueCzk { get; set; }
        public decimal ExchangeRate { get; set; }

        [JsonPropertyName("_attributes")]
        public GridDataAttributes? GridRowAttributes { get;set;}
    }
}