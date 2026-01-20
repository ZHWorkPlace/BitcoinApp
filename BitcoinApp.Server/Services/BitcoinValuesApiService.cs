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
            return [.. retrievedValuesService.GetRetrievedValues().Select(value => new GetRetrievedValueDto
            {
                Id = value.Id,
                IsSaveEnabled = !value.IsSaved,
                RetrievedAt = value.RetrievedAt,
                ValueCzk = value.ValueCzk,
                ValueEur = value.ValueEur,
                ExchangeRate = value.ExchangeRate,
            })];
        }

        public async Task<bool> SaveRetrievedBitcoinValueAsync(Guid id)
        {
            var exists = await bitcoinValueRepository.ExistsAsync(id);
            if (exists)
            {
                //throw new Exception($"Value {id} already exists in the database.");
                return false;
            }

            var data = retrievedValuesService.GetRetrievedValues().SingleOrDefault(v => v.Id == id);
            if (data is null)
            {
                //throw new Exception($"Value {id} not found in retrieveds");
                 return false;
            }

            var result = await bitcoinValueRepository.AddAsync(id, data.RetrievedAt, data.ValueEur, data.ValueCzk, data.ExchangeRate, string.Empty);
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
                Id = d.Id,
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
                await bitcoinValueRepository.UpdateNoteAsync(record.Id, record.Note);
            }
        }

        public async Task DeleteBitcoinValueRecordsAsync(List<DeleteValueRecordDto> delete)
        {
            foreach (var record in delete)
            {
                if (await bitcoinValueRepository.DeleteAsync(record.Id))
                {
                    retrievedValuesService.GetRetrievedValues().SingleOrDefault(v => v.Id == record.Id)?.IsSaved = false;
                }
            }
        }
    }
}