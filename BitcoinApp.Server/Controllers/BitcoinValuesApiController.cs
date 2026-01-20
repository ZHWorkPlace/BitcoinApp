using BitcoinApp.Api.Dto;
using BitcoinApp.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BitcoinApp.Server.Controllers
{
    [ApiController]
    [Route("api")]
    public class BitcoinValuesApiController : ControllerBase
    {
        private readonly BitcoinValuesApiService bitcoinValuesApiService;
        public BitcoinValuesApiController(BitcoinValuesApiService bitcoinValuesApiService)
        {
            this.bitcoinValuesApiService = bitcoinValuesApiService;
        }


        [HttpGet("Live")]
        public async Task<ActionResult<GetRetrievedValuesResponse>> GetRetrievedBitcoinValuesAsync()
        {
            var result = new GetRetrievedValuesResponse
            {
                Retrieved = await bitcoinValuesApiService.GetRetrievedBitcoinValuesAsync()
            };

            return Ok(result);
        }


        [HttpPost("Live")]
        public async Task<ActionResult<bool>> SaveRetrievedBitcoinValueAsync(SaveRetrievedValueDto dto)
        {
            var result = await bitcoinValuesApiService.SaveRetrievedBitcoinValueAsync(dto);

            return Ok(result);
        }


        [HttpGet("Saved")]
        public async Task<ActionResult<GetValueRecordsResponse>> GetBitcoinValueRecordsAsync()
        {
            var result = new GetValueRecordsResponse
            {
                Records = await bitcoinValuesApiService.GetBitcoinValueRecordsAsync()
            };

            return Ok(result);
        }


        [HttpPost("Saved/update")]
        public async Task<ActionResult> UpdateSavedBitcoinValuesAsync(UpdateValueRecordsRequest request)
        {
            await bitcoinValuesApiService.UpdateBitcoinValueRecordsAsync(request.Data);

            return Ok();
        }


        [HttpPost("Saved/delete")]
        public async Task<ActionResult> DeleteSavedBitcoinValuesAsync(DeleteValueRecordsRequest request)
        {
            await bitcoinValuesApiService.DeleteBitcoinValueRecordsAsync(request.Data);

            return Ok();
        }
    }
}