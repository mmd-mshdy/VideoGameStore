using VideoGameStore.Domain.Entities;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
namespace VideoGameStore.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GameService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(CreateGameDto dto)
        {
            var game = new Game
            {
                Name = dto.Name,
                Price = dto.Price,
                Genre = dto.Genre,
                ReleaseDate = dto.ReleaseDate,
            };
            await _unitOfWork.Games.AddAsync(game);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(int Id,UpdateGameDto dto)
        {

            var game = await _unitOfWork.Games.GetByIdAsync(Id);
            if (game == null) return;

            game.Name = dto.Name;
            game.Genre = dto.Genre;
            game.Price = dto.Price;
            game.ReleaseDate = dto.ReleaseDate;

            await _unitOfWork.Games.UpdateAsync(game);
            await _unitOfWork.CompleteAsync();
        }



        public async Task<GameDto?> GetByIdAsync(int Id)
        {
            var game = await _unitOfWork.Games.GetByIdAsync(Id);
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
            var games = await _unitOfWork.Games.GetAllAsync();
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
            await _unitOfWork.Games.DeleteAsync(Id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
