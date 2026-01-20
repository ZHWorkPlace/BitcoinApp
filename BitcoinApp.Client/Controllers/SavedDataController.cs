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
                 await bitcoinApiService.DeleteSavedData(data.DeletedRows);

                 return new GridDataResultModel();
            }
            catch (Exception ex)
            {
                return new GridDataResultModel(ex.Message);
            }
        }
    }
}