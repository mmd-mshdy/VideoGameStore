using VideoGameStore.Application.Games.Command.Create;
using VideoGameStore.Application.Games.Command.Update;
using VideoGameStore.Domain.Entities;
namespace VideoGameStore.Application.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetAllAsync();
        Task<GameDto> GetByIdAsync(int Id);
        Task AddAsync(CreateGameCommand dto);
        Task UpdateAsync(int Id ,UpdateGameCommand dto);
        Task DeleteAsync(int Id);
    }
}
