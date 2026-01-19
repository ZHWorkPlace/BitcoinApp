using BitcoinApp.Api;
using BitcoinApp.Client.Models;
using BitcoinApp.Client.Models.LiveData;
using BitcoinApp.Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace BitcoinApp.Client.Controllers
{
    public class LiveDataController : Controller
    {
        private readonly BitcoinApiService bitcoinApiService;

        public LiveDataController(BitcoinApiService bitcoinApiService)
        {
            this.bitcoinApiService = bitcoinApiService;
        }


        public async Task<IActionResult> Index()
        {
            return View();
        }


        [HttpGet]
        [Produces("application/json")]
        public async Task<GridDataSourceModel<LiveDataValueViewModel>> GetData(int page)
        {
            return await bitcoinApiService.GridDataSource(async (s) => await s.GetLiveData(), page);
        }
    }
}
