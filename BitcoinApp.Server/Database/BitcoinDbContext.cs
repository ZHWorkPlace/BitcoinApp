using Microsoft.EntityFrameworkCore;

namespace BitcoinApp.Server.Database
{
    public class BitcoinDbContext : DbContext
    {
        public BitcoinDbContext(DbContextOptions<BitcoinDbContext> options) : base(options)
        {
        }

        public DbSet<Dto.BitcoinValueRecord> BitcoinValueRecords { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Dto.BitcoinValueRecord>()
               .HasKey(i => i.RetrievedAt);

            modelBuilder.Entity<Dto.BitcoinValueRecord>()
               .Property(i => i.RetrievedAt)
               .HasColumnType("datetime")
               .IsRequired();

            modelBuilder.Entity<Dto.BitcoinValueRecord>()
              .Property(i => i.BitcoinValue)
              .HasColumnType("decimal(8,2)")
              .IsRequired();

            modelBuilder.Entity<Dto.BitcoinValueRecord>()
              .Property(i => i.Note)
              .HasColumnType("nvarchar(MAX)");
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("server=.;database=myDb;trusted_connection=true;");
        //}
    }
}