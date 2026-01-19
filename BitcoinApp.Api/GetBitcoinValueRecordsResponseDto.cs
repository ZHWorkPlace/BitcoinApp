using System.Collections.Generic;

namespace BitcoinApp.Api
{
    public class GetBitcoinValueRecordsResponseDto
    {
        public List<BitcoinValueRecordDto> Records { get; set; } = new List<BitcoinValueRecordDto>();
    }
}