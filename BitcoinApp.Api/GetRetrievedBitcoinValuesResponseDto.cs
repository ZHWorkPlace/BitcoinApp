using System.Collections.Generic;

namespace BitcoinApp.Api
{
    public class GetRetrievedBitcoinValuesResponseDto
    {
        public List<BitcoinValueRetrievedDto> Retrieved { get; set; } = new List<BitcoinValueRetrievedDto>();
    }
}