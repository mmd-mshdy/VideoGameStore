using VideoGameStore.Domain.Entities;
using VideoGameStore.Application.Interfaces;
namespace VideoGameStore.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;
        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Game game)
        {
            var existing = await _repository.GetByIdAsync(game.Id);
            if (existing != null)
            {
               throw new InvalidOperationException("game already exists");
            }
            await _repository.AddAsync(game);
        
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing != null)
            {
                await _repository.DeleteAsync(id);
            }
            throw new InvalidOperationException("Game not Found");
           
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
          return  await _repository.GetAllAsync();
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing != null)
            {
               return await _repository.GetByIdAsync(id);
            }
            throw new InvalidOperationException("Game not found");
        }

        public async Task<IEnumerable<Game>> GetGameAsync() => await _repository.GetAllAsync();

        public async Task UpdateAsync(Game game)
        {
            await _repository.UpdateAsync(game);
        }
    }
}
