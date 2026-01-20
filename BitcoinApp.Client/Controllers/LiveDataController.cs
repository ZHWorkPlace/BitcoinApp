using BitcoinApp.Api;
using BitcoinApp.Client.Models;
using BitcoinApp.Client.Models.LiveData;
using BitcoinApp.Client.Models.SavedData;
using BitcoinApp.Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

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

        [HttpPut]
        [Produces("application/json")]
        public async Task UpdateData([FromBody] GridDataUpdatedModel<LiveDataValueViewModel> grid)
        {
            

            //return await bitcoinApiService.GridDataSource(async (s) => await s.GetSavedData(), 14);
        }

        [HttpPut]
        [Produces("application/json")]
        public async Task<GridDataSourceModel<SavedDataRecordViewModel>> UpdateData([FromBody] object obj)
        {


            return await bitcoinApiService.GridDataSource(async (s) => await s.GetSavedData(), 14);
        }
    }
}
