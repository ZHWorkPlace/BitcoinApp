
using BitcoinApp.Server.Database.Dto;

namespace BitcoinApp.Server.Database
{
    public interface IBitcoinValueRepository
    {
        Task<bool> AddAsync(DateTime retrievedAt, decimal bitcoinValue, string note, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(DateTime retrievedAt, CancellationToken cancellationToken = default);
        Task<bool> UpdateNoteAsync(DateTime retrievedAt, string note, CancellationToken cancellationToken = default);
        Task<List<BitcoinValueRecord>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}