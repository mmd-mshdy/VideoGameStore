
using VideoGameStore.Application.Dtos;
using VideoGameStore.Domain.Entities;
namespace VideoGameStore.Application.Interfaces
{


    public static class GameMappings
    {
        public static GameDto ToDto(this Game game)
            => new(
                game.Id,
                game.Name,
                game.Genre,
                game.Price.Amount,
                game.IsAvailable
            );
    }
}
