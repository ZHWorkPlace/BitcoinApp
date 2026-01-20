using BitcoinApp.Api.Dto;
using BitcoinApp.Server.Database;

namespace BitcoinApp.Server.Services
{
    public class BitcoinValuesApiService
    {
        private readonly IBitcoinValueRepository bitcoinValueRepository;
        private readonly IRetrievedValuesService retrievedValuesService;

        public BitcoinValuesApiService(IBitcoinValueRepository bitcoinValueRepository, IRetrievedValuesService retrievedValuesService)
        {
            this.bitcoinValueRepository = bitcoinValueRepository;
            this.retrievedValuesService = retrievedValuesService;
        }

        public async Task<List<GetRetrievedValueDto>> GetRetrievedBitcoinValuesAsync()
        {
            var x = retrievedValuesService.GetRetrievedValues().Select(value => new GetRetrievedValueDto
            {
                IsSaveEnabled = !value.IsSaved,
                RetrievedAt = value.RetrievedAt,
                ValueCzk = value.ValueCzk,
                ValueEur = value.ValueEur,
                ExchangeRate = value.ExchangeRate,
            }).ToList();

            //if (x.Count > 3)
            //{
            //    x[2].IsSaveEnabled = false;
            //}

            return x;
        }

        public async Task<bool> SaveRetrievedBitcoinValueAsync(DateTime retrievedAt)
        {
            var exists = await bitcoinValueRepository.ExistsAsync(retrievedAt);
            if (exists)
            {
                return false;
            }

            var data = retrievedValuesService.GetRetrievedValues().FirstOrDefault(v => v.RetrievedAt == retrievedAt);
            if (data is null)
            {
                return false;
            }

            var result = await bitcoinValueRepository.AddAsync(data.RetrievedAt, data.ValueEur, data.ValueCzk, data.ExchangeRate, string.Empty);
            if (result)
            {
                data.IsSaved = true;
            }

            return result;
        }


        public async Task<List<GetValueRecordDto>> GetBitcoinValueRecordsAsync(CancellationToken cancellationToken = default)
        {
            var data = await bitcoinValueRepository.GetAllAsync(cancellationToken);

            return [.. data.Select(d => new GetValueRecordDto
            {
                RetrievedAt = d.RetrievedAt,
                ValueCzk = d.ValueCzk,
                ValueEur = d.ValueEur,
                ExchangeRate = d.ExchangeRate,
                Note = d.Note
            })];
        }

        public async Task UpdateBitcoinValueRecordsAsync(List<UpdateValueRecordDto> update)
        {
            foreach (var record in update)
            {
                await bitcoinValueRepository.UpdateNoteAsync(record.RetrievedAt, record.Note);
            }
        }

        public async Task DeleteBitcoinValueRecordsAsync(List<DeleteValueRecordDto> delete)
        {
            foreach (var record in delete)
            {
                await bitcoinValueRepository.DeleteAsync(record.RetrievedAt);
            }
        }
    }
}