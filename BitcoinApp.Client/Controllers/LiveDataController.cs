using BitcoinApp.Api.Dto;
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
            var model = new LiveDataViewModel
            {
                Grid = await GetLiveDataGridViewModel()
            };

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> GetGridView()
        {
            return PartialView("_LiveDataGrid", await GetLiveDataGridViewModel());
        }


        [HttpPost]
        [Produces("application/json")]
        public async Task<bool> SaveLiveData([FromBody] SaveRetrievedValueRequest request)
        {
            return await bitcoinApiService.SaveLiveData(request);
        }


        private async Task<LiveDataGridViewModel> GetLiveDataGridViewModel()
        {
            var data = await bitcoinApiService.GetLiveData();

            return new LiveDataGridViewModel
            {
                Rows = [.. data.Select(item => new LiveDataGridRowViewModel
                    {
                        Id = item.Id,
                        IsSaveEnabled = item.IsSaveEnabled,
                        RetrievedAt = item.RetrievedAt,
                        ValueCzk = item.ValueCzk,
                        ValueEur = item.ValueEur,
                        ExchangeRate = item.ExchangeRate
                    })]
            };
        }
    }
}