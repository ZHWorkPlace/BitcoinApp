using System.Collections.Generic;

namespace BitcoinApp.Api.Dto
{
    public class UpdateValueRecordsRequest
    {
        public List<UpdateValueRecordDto> Data { get; set; } = new List<UpdateValueRecordDto>();
    }
}