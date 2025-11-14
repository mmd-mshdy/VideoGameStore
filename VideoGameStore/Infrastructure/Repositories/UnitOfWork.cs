using NuGet.Protocol.Core.Types;
using VideoGameStore.Application.Games.Command.Update;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.Entities;
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
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task AddGames(UpdateGameCommand dto)
        {
            var game = new Game(dto.Name , dto.Genre ,dto.Price ,dto.ReleaseDate);
            if (game!=null)
            await _context.AddAsync(game);
            throw new InvalidDataException("Invalid");
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
