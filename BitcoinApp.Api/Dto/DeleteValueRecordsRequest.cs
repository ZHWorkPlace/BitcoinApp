using System.Collections.Generic;

namespace BitcoinApp.Api.Dto
{
    public class DeleteValueRecordsRequest
    {
        public List<DeleteValueRecordDto> Data { get; set; } = new List<DeleteValueRecordDto>();
    }
}