using System;

namespace BitcoinApp.Api
{
    public class SaveBitcoinValueRetrievedDto
    {
        public DateTime RetrievedAt { get; set; }
        public decimal Value { get; set; }
    }
}