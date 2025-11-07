using Microsoft.EntityFrameworkCore;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Infrastructure.Data
{
    public class VideoGamesContext : DbContext
    {
        public VideoGamesContext(DbContextOptions<VideoGamesContext> options) : base(options) { }
        public DbSet<Game>? Games { get; set; } = null;
    }
}
