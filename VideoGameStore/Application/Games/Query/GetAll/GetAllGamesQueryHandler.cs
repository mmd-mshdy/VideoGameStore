using VideoGameStore.Application.Dtos;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace VideoGameStore.Application.Games.Query.GetAll
{
    public sealed class GetAllGamesQueryHandler : IQueryHandler<GetAllGamesQuery ,Result<IReadOnlyList<GameDto>>>
    {
        private readonly IGenericRepository<Game> _games;
        public GetAllGamesQueryHandler(IGenericRepository<Game> games)
        {
            _games = games;
        }

        public Task<Result<IReadOnlyList<GameDto>>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
        {
            var games = _games.GetAllAsync();


            return Result.Success<IReadOnlyList<GameDto>>(games
            .Select(g => g.ToDto())
            .ToList());
        }
    }
}
