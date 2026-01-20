using System;

namespace BitcoinApp.Api.Dto
{
    public class GetRetrievedValueDto
    {
        public bool IsSaveEnabled { get; set; }
        public DateTime RetrievedAt { get; set; }
        public decimal ValueEur { get; set; }
        public decimal ValueCzk { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}