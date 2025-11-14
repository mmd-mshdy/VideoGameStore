using Microsoft.EntityFrameworkCore;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.Entities;
using VideoGameStore.Infrastructure.Data;

namespace VideoGameStore.Infrastructure.Repositories
{
    public class ManagerRepository :IGenericRepository<Manager>
    {

        private readonly VideoGamesContext _context;
        public ManagerRepository(VideoGamesContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Manager t)
        {
            await _context.Managers.AddAsync(t);

        }

        public async Task DeleteAsync(int id)
        {
            var manager = await _context.Managers.FirstOrDefaultAsync(x => x.Id == id);
            if (manager is null) throw new InvalidOperationException("invalid");
            _context.Managers.Remove(manager);

        }

        public async Task<IEnumerable<Manager>> GetAllAsync()
        {
            return await _context.Managers.ToListAsync();
        }

        public async Task<Manager> GetByIdAsync(int id)
        {
             return await _context.Managers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task UpdateAsync(Manager t)
        {
            _context.Managers.Update(t);
            return Task.CompletedTask;
        }
    }
}
