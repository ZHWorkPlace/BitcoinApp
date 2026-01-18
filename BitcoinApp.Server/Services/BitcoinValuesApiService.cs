using BitcoinApp.Api;
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

        public async Task<List<BitcoinValueRetrievedDto>> GetRetrievedBitcoinValuesAsync()
        {
            return retrievedValuesService.GetRetrievedValues().Select(value => new BitcoinValueRetrievedDto
            {
                IsSaveEnabled = !value.IsSaved,
                RetrievedAt = value.RetrievedAt,
                Value = value.Value
            }).ToList();
        }

        public async Task<bool> SaveRetrievedBitcoinValueAsync(SaveBitcoinValueRetrievedDto data)
        {
            var result = await bitcoinValueRepository.AddAsync(data.RetrievedAt, data.Value, string.Empty);
            if (result)
            {
                retrievedValuesService.MarkAsSaved(data.RetrievedAt);
            }

            return result;
        }


        public async Task<List<BitcoinValueRecordDto>> GetBitcoinValueRecordsAsync(CancellationToken cancellationToken = default)
        {
            var data = await bitcoinValueRepository.GetAllAsync(cancellationToken);

            return data.Select(d => new BitcoinValueRecordDto
            {
                RetrievedAt = d.RetrievedAt,
                BitcoinValue = d.BitcoinValue,
                Note = d.Note
            }).ToList();
        }

        public async Task UpdateBitcoinValueRecordsAsync(List<BitcoinValueRecordDto> records)
        {
            foreach (var record in records)
            {
                await bitcoinValueRepository.UpdateNoteAsync(record.RetrievedAt, record.Note);
            }
        }

        public async Task DeleteBitcoinValueRecordsAsync(List<BitcoinValueRecordDto> records)
        {
            foreach (var record in records)
            {
                await bitcoinValueRepository.DeleteAsync(record.RetrievedAt);
            }
        }
    }
}