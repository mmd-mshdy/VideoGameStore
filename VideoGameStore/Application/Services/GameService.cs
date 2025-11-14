using VideoGameStore.Domain.Entities;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Application.Dtos;
namespace VideoGameStore.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IUnitOfWork _unitOfWork;
        public GameService(IUnitOfWork unitOfWork , IGameRepository gameRepository)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(CreateGameDto dto)
        {
            var game = new Game
            (
                dto.Name,
                 dto.Genre,
                dto.Price,

                 dto.ReleaseDate
            );
            await _gameRepository.AddAsync(game);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(int Id,UpdateGameDto dto)
        {

            var game = await _gameRepository.GetByIdAsync(Id);
            if (game == null) return;
            await _gameRepository.UpdateAsync(game);
            await _unitOfWork.CompleteAsync();
        }



        public async Task<GameDto?> GetByIdAsync(int Id)
        {
            var game = await _gameRepository.GetByIdAsync(Id);
            if (game == null) return null;

            return new GameDto
            (
                game.Id,
                game.Name,
                game.Genre,
                game.Price,
                game.ReleaseDate
            );
        }
        public async Task<IEnumerable<GameDto>> GetAllAsync()
        {
            var games = await _gameRepository.GetAllAsync();
            return games.Select(g => new GameDto(
                g.Id,
                g.Name,       
                g.Genre,
                g.Price,
                g.ReleaseDate
            ));
        }

        public async Task DeleteAsync(int Id)
        {
            await _gameRepository.DeleteAsync(Id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
