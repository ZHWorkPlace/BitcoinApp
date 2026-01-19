using System;

namespace BitcoinApp.Api
{
    public class BitcoinValueRetrievedDto
    {
        public bool IsSaveEnabled { get; set; }
        public DateTime RetrievedAt { get; set; }
        public decimal Value { get; set; }
    }
}