using BitcoinApp.Api;
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
        public async Task<ActionResult<List<BitcoinValueRetrievedDto>>> GetRetrievedBitcoinValuesAsync()
        {
            var result = await bitcoinValuesApiService.GetRetrievedBitcoinValuesAsync();

            return Ok(result);
        }


        [HttpPost("Live")]
        public async Task<ActionResult<bool>> SaveRetrievedBitcoinValueAsync(SaveBitcoinValueRetrievedDto dto)
        {
            var result = await bitcoinValuesApiService.SaveRetrievedBitcoinValueAsync(dto);

            return Ok(result);
        }


        [HttpGet("Saved")]
        public async Task<ActionResult<List<BitcoinValueRetrievedDto>>> GetSavedBitcoinValuesAsync()
        {
            var result = await bitcoinValuesApiService.GetRetrievedBitcoinValuesAsync();

            return Ok(result);
        }


        [HttpPost("Saved/update")]
        public async Task<ActionResult> UpdateSavedBitcoinValuesAsync(List<BitcoinValueRecordDto> toUpdate)
        {
            await bitcoinValuesApiService.UpdateBitcoinValueRecordsAsync(toUpdate);

            return Ok();
        }


        [HttpPost("Saved/delete")]
        public async Task<ActionResult> DeleteSavedBitcoinValuesAsync(List<BitcoinValueRecordDto> toDelete)
        {
            await bitcoinValuesApiService.DeleteBitcoinValueRecordsAsync(toDelete);

            return Ok();
        }
    }
}