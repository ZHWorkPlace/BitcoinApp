using System.Collections.Generic;

namespace BitcoinApp.Api.Dto
{
    public class GetRetrievedValuesResponse
    {
        public List<GetRetrievedValueDto> Retrieved { get; set; } = new List<GetRetrievedValueDto>();
    }
}