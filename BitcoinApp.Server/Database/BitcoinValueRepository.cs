using BitcoinApp.Server.Database.Dto;
using Microsoft.EntityFrameworkCore;

namespace BitcoinApp.Server.Database
{
    public class BitcoinValueRepository : IBitcoinValueRepository
    {
        private readonly BitcoinDbContext _db;

        public BitcoinValueRepository(BitcoinDbContext db)
        {
            _db = db;
        }


        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.BitcoinValueRecords.FindAsync(id, cancellationToken) != null;
        }


        public async Task<bool> AddAsync(Guid id, DateTime retrievedAt, decimal valueEur, decimal valueCzk, decimal exchangeRate, string note, CancellationToken cancellationToken = default)
        {
            try
            {
                var record = new BitcoinValueRecord
                {
                    Id = id,
                    RetrievedAt = retrievedAt,
                    ValueEur = valueEur,
                    ValueCzk = valueCzk,
                    ExchangeRate = exchangeRate,
                    Note = note
                };

                await _db.BitcoinValueRecords.AddAsync(record, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.BitcoinValueRecords.FindAsync(id, cancellationToken);
            if (entity is null)
            {
                return false;
            }

            _db.BitcoinValueRecords.Remove(entity);

            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> UpdateNoteAsync(Guid id, string note, CancellationToken cancellationToken = default)
        {
            var entity = await _db.BitcoinValueRecords.FindAsync(id, cancellationToken);
            if (entity is null)
            {
                return false;
            }

            entity.Note = note;

            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }

        public Task<List<BitcoinValueRecord>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _db.BitcoinValueRecords
                      .AsNoTracking()
                      .OrderByDescending(r => r.RetrievedAt)
                      .ToListAsync(cancellationToken);
        }


        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _db.SaveChangesAsync(cancellationToken);
        }
    }
}