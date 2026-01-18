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

        public async Task<bool> AddAsync(DateTime retrievedAt, decimal bitcoinValue, string note, CancellationToken cancellationToken = default)
        {
            try
            {
                var record = new BitcoinValueRecord
                {
                    RetrievedAt = retrievedAt,
                    BitcoinValue = bitcoinValue,
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

        public async Task<bool> DeleteAsync(DateTime retrievedAt, CancellationToken cancellationToken = default)
        {
            var entity = await _db.BitcoinValueRecords.FindAsync(retrievedAt, cancellationToken);
            if (entity is null)
            {
                return false;
            }

            _db.BitcoinValueRecords.Remove(entity);

            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> UpdateNoteAsync(DateTime retrievedAt, string note, CancellationToken cancellationToken = default)
        {
            var entity = await _db.BitcoinValueRecords.FindAsync(retrievedAt, cancellationToken);
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