using System.Collections.Generic;

namespace BitcoinApp.Api.Dto
{
    public class SaveRetrievedValuesRequest
    {
        public List<SaveRetrievedValueDto> DataToSave { get; set; } = new List<SaveRetrievedValueDto>();
    }
}