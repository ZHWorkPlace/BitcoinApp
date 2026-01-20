using BitcoinApp.Api.Dto;
using BitcoinApp.Client.Models;
using BitcoinApp.Client.Models.LiveData;
using BitcoinApp.Client.Models.SavedData;

namespace BitcoinApp.Client.Services
{
    public class BitcoinApiService
    {
        private readonly HttpClient httpClient;

        public BitcoinApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public async Task<List<LiveDataGridRowViewModel>> GetLiveData()
        {
            var data = await httpClient.GetFromJsonAsync<GetRetrievedValuesResponse>("Live");

            return data?.Retrieved.Select(item => new LiveDataGridRowViewModel
            {
                Id = item.Id,
                IsSaveEnabled = item.IsSaveEnabled,
                RetrievedAt = item.RetrievedAt,
                ValueCzk = item.ValueCzk,
                ValueEur = item.ValueEur,
                ExchangeRate = item.ExchangeRate
            }).ToList() 
            ?? throw new Exception("GetLiveData: empty data");
        }


        public async Task<bool> SaveLiveData(SaveRetrievedValueRequest request)
        {
            var response = await httpClient.PostAsJsonAsync("Live", request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<bool>();
        }


        public async Task<List<SavedDataRecordViewModel>> GetSavedData()
        {
            var data = await httpClient.GetFromJsonAsync<GetValueRecordsResponse>("Saved");

            return data?.Records.Select(record => new SavedDataRecordViewModel
            {
                RetrievedAt = record.RetrievedAt,
                ValueCzk = record.ValueCzk,
                ValueEur = record.ValueEur,
                ExchangeRate = record.ExchangeRate,
                Note = record.Note
            }).ToList() 
            ?? throw new Exception("GetSavedData: empty data");
        }


        public async Task UpdateSavedData(List<SavedDataRecordViewModel> data)
        {
            var request = new UpdateValueRecordsRequest
            {
                Data = [.. data.Select(record => new UpdateValueRecordDto
                {
                    RetrievedAt = record.RetrievedAt,
                    Note = record.Note
                })]
            };

            var response = await httpClient.PostAsJsonAsync("Saved/update", request);

            response.EnsureSuccessStatusCode();
        }


        public async Task DeleteSavedData(List<SavedDataRecordViewModel> data)
        {
            var request = new DeleteValueRecordsRequest
            {
                Data = [.. data.Select(record => new DeleteValueRecordDto
                {
                    RetrievedAt = record.RetrievedAt
                })]
            };

            var response = await httpClient.PostAsJsonAsync("Saved/delete", request);

            response.EnsureSuccessStatusCode();
        }


        public async Task<GridDataSourceModel<T>> GridDataSource<T>(Func<BitcoinApiService, Task<List<T>>> getData, int page)
        {
            var data = await getData(this);

            return new GridDataSourceModel<T>
            {
                Result = true,
                Data = new GridDataSourceDataModel<T>
                {
                    Contents = data,
                    Pagination = new GridDataSourcePaginationModel
                    {
                        Page = page,
                        TotalCount = data.Count
                    }
                }
            };
        }
    }
}