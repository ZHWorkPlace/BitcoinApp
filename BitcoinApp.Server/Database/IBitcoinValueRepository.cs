using BitcoinApp.Server.Database.Dto;

namespace BitcoinApp.Server.Database
{
    public interface IBitcoinValueRepository
    {
        Task<bool> ExistsAsync(DateTime retrievedAt, CancellationToken cancellationToken = default);
        Task<bool> AddAsync(DateTime retrievedAt, decimal valueEur, decimal valueCzk, decimal exchangeRate, string note, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(DateTime retrievedAt, CancellationToken cancellationToken = default);
        Task<bool> UpdateNoteAsync(DateTime retrievedAt, string note, CancellationToken cancellationToken = default);
        Task<List<BitcoinValueRecord>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}