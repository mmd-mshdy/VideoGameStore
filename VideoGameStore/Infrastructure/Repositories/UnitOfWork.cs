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
        private readonly IGenericRepository<Game> _repository;
        public UnitOfWork(VideoGamesContext context , IGenericRepository<Game> repository)
        {
            _context = context;
            _repository = repository;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task AddGames(UpdateGameCommand dto)
        {
            var game = new Game(dto.GameDto.Name , dto.GameDto.Genre , dto.GameDto.Price , dto.GameDto.ReleaseDate);
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
