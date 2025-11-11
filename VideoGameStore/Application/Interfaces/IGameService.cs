using VideoGameStore.Application.Dtos;
using VideoGameStore.Domain.Entities;
namespace VideoGameStore.Application.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetAllAsync();
        Task<GameDto> GetByIdAsync(int Id);
        Task AddAsync(CreateGameDto dto);
        Task UpdateAsync(int Id ,UpdateGameDto dto);
        Task DeleteAsync(int Id);
    }
}
