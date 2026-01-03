using Microsoft.EntityFrameworkCore;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Infrastructure.Data;

namespace VideoGameStore.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly VideoGamesContext _context;

    public GenericRepository(VideoGamesContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
        => await _context.Set<T>().AsNoTracking().ToListAsync();

    public async Task<T?> GetByIdAsync(int id)
        => await _context.Set<T>().FindAsync(id);

    public async Task AddAsync(T entity)
        => await _context.Set<T>().AddAsync(entity);

    public async Task UpdateAsync(T entity)
    {
        _context.Update(entity);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
          _context.Remove(id);
        await Task.CompletedTask;

    }
}
