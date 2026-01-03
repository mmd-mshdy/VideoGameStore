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
        public DbSet<Rental>? Rentals { get; set; } = null;

        public async Task CompleteAsync()
        {
            await base.SaveChangesAsync();
        }
    }
}
