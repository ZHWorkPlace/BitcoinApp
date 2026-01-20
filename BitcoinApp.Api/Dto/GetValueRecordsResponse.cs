using System.Collections.Generic;

namespace BitcoinApp.Api.Dto
{
    public class GetValueRecordsResponse
    {
        public List<GetValueRecordDto> Records { get; set; } = new List<GetValueRecordDto>();
    }
}