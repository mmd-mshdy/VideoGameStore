using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.Entities;
using VideoGameStore.Infrastructure.Data;
namespace VideoGameStore.Infrastructure.Repositories
{
    public class GameRepositories: IGameRepository
    {
        private readonly VideoGamesContext _context;
        public GameRepositories(VideoGamesContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Game>> GetAllAsync() => await _context.Games.ToListAsync();
        public async Task<Game> FindByIdAsync(int id) => await _context.Games.FindAsync(id);
        public async Task Add(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();

        }
        public async Task Update(Game game)
        {
            _context.Games.Update(game);
            await _context.SaveChangesAsync();

        }
        public async Task Delete (Game game)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }
    }
}
