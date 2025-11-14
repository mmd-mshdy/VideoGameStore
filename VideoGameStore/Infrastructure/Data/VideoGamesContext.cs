using Microsoft.EntityFrameworkCore;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Infrastructure.Data
{
    public class VideoGamesContext : DbContext , IUnitOfWork
    {
        public VideoGamesContext(DbContextOptions<VideoGamesContext> options) : base(options) { }
        public DbSet<Game>? Games { get; set; } = null;
        public DbSet<Customer>? Customers { get; set; } = null; 
        public DbSet<Manager>? Managers { get; set; } = null; 
        public DbSet<Transaction>? Transactions { get; set; } = null; 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().Property(x => x.Amount).HasPrecision(18,2);
            modelBuilder.Entity<Game>().Property(x => x.Price).HasPrecision(18,2);
            modelBuilder.Entity<Customer>().Property(x => x.WalletBalance).HasPrecision(18,2);
;        }

        public async Task CompleteAsync()
        {
            await base.SaveChangesAsync();
        }
    }
}
