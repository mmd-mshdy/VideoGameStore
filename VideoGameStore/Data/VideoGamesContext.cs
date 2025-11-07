using Microsoft.EntityFrameworkCore;
using VideoGameStore.Dtos;
namespace VideoGameStore.Data
{
    public class VideoGamesContext : DbContext
    {
        public VideoGamesContext(DbContextOptions<VideoGamesContext> options) : base(options) { }
        public DbSet<GameDto>? GameDtos { get; set; } = null;
    }
}
