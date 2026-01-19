using BitcoinApp.Api;
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


        


        public async Task<SavedDataViewModel> GetSavedData2()
        {
            var data = await httpClient.GetFromJsonAsync<GetBitcoinValueRecordsResponseDto>("Saved");
            if (data != null)
            {
                return new SavedDataViewModel
                {
                    Result = true,
                    Data = data.Records.Select(record => new SavedDataRecordViewModel
                    {
                        RetrievedAt = record.RetrievedAt,
                        BitcoinValue = record.BitcoinValue,
                        Note = record.Note
                    }).ToList()
                };
            }
            
            throw new Exception("GetSavedData: empty data");
        }

        public async Task<List<LiveDataValueViewModel>> GetLiveData()
        {
            var data = await httpClient.GetFromJsonAsync<GetRetrievedBitcoinValuesResponseDto>("Live");

            return data?.Retrieved.Select(item => new LiveDataValueViewModel
            {
                GridRowAttributes = new GridDataSourceDataAttributes
                {
                    CheckDisabled = !item.IsSaveEnabled
                },
                RetrievedAt = item.RetrievedAt,
                Value = item.Value
            }
            ).ToList() ?? throw new Exception("GetLiveData: empty data");
        }

        public async Task<List<BitcoinValueRecordDto>> GetSavedData()
        {
            var data = await httpClient.GetFromJsonAsync<GetBitcoinValueRecordsResponseDto>("Saved");

            return data?.Records ?? throw new Exception("GetSavedData: empty data");
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