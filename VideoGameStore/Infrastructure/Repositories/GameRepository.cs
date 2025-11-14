using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.Entities;
using VideoGameStore.Infrastructure.Data;
namespace VideoGameStore.Infrastructure.Repositories
{
    public class GameRepository : IGenericRepository<Game>
    {
        private readonly VideoGamesContext _context;
        public GameRepository(VideoGamesContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Game>> GetAllAsync() => await _context.Games.ToListAsync();
        public async Task<Game> GetByIdAsync(int id) => await _context.Games.FindAsync(id);
        public async Task AddAsync(Game game)
        {
            await _context.Games.AddAsync(game);

        }
        public async Task UpdateAsync(Game game)
        {
              _context.Games.Update(game);

        }
        public async  Task DeleteAsync (int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null) 
            {
                _context.Games.Remove(game);
            }
        }
    }
}
