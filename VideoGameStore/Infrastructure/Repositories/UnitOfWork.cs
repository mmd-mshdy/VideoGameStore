using NuGet.Protocol.Core.Types;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Infrastructure.Data;

namespace VideoGameStore.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VideoGamesContext _context;
        public IGameRepository Games { get; }
        public UnitOfWork(VideoGamesContext context , IGameRepository repository)
        {
            _context = context;
            Games = repository;
        }
        public async Task<int> CompleteAsync()
        {
           return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
