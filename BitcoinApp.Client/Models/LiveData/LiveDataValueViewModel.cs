using System.Text.Json.Serialization;

namespace BitcoinApp.Client.Models.LiveData
{
    public class LiveDataValueViewModel
    {
        public DateTime RetrievedAt { get; set; }
        public decimal Value { get; set; }

        [JsonPropertyName("_attributes")]
        public GridDataSourceDataAttributes? GridRowAttributes { get;set;}
    }
}