using VideoGameStore.Domain.Entities;
namespace VideoGameStore.Application.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game> GetByIdAsync(int id);
        Task AddAsync (Game game);
        Task UpdateASync(Game game);
        Task DeleteAsync(int id);
        
    }
}
