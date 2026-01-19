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
        public async Task<GridDataSourceModel<BitcoinValueRecordDto>> GetData(int page)
        {
            return await bitcoinApiService.GridDataSource(async (s) => await s.GetSavedData(), page);
        }
    }
}