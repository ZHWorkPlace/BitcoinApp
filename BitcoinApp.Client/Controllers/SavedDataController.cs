using BitcoinApp.Api;
using BitcoinApp.Client.Models;
using BitcoinApp.Client.Models.SavedData;
using BitcoinApp.Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace BitcoinApp.Client.Controllers
{
    public class SavedDataController : Controller
    {
        private readonly BitcoinApiService bitcoinApiService;

        public SavedDataController(BitcoinApiService bitcoinApiService)
        {
            this.bitcoinApiService = bitcoinApiService;
        }


        public async Task<IActionResult> Index()
        {
            return View();
        }


        [HttpGet]
        [Produces("application/json")]
        public async Task<GridDataResultModel> GetData(int page)
        {
            return await bitcoinApiService.GridDataSource(async (s) => await s.GetSavedData(), page);
        }


        [HttpPost]
        [Produces("application/json")]
        public async Task<GridDataResultModel> UpdateData([FromBody] GridDataUpdatedModel<SavedDataRecordViewModel> data)
        {
            try
            {
                await bitcoinApiService.UpdateSavedData(data.UpdatedRows);

                return new GridDataResultModel("SHIT");
            }
            catch (Exception ex)
            {
                return new GridDataResultModel(ex.Message);
            }
        }


        [HttpPost]
        [Produces("application/json")]
        public async Task<GridDataResultModel> DeleteData([FromBody] GridDataDeleteModel<SavedDataRecordViewModel> data)
        {
            try
            {
               // await bitcoinApiService.DeleteSavedData(data.DeletedRows);

                 return new GridDataResultModel();
            }
            catch (Exception ex)
            {
                return new GridDataResultModel(ex.Message);
            }
        }


        // Added endpoint to receive selected row ids from the LiveData grid.
        // Accepts a JSON array of integers in the request body and returns a simple result object.
        [HttpPost]  
        [Route("SavedData/SubmitSelected")]
        public async Task<IActionResult> SubmitSelected([FromBody] int[] ids)
        {
            if (ids == null || ids.Length == 0)
            {
                return BadRequest("No row ids provided.");
            }

            // TODO: replace with actual processing logic that uses bitcoinApiService
            // Example: await bitcoinApiService.ProcessSavedData(ids);

            // For now return simple success with count
            return Ok(new { processed = ids.Length });
        }
    }
}