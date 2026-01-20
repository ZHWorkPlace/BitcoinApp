using System;

namespace BitcoinApp.Api.Dto
{
    public class UpdateValueRecordDto
    {
        public Guid Id { get; set; }
        public string Note { get; set; }
    }
}