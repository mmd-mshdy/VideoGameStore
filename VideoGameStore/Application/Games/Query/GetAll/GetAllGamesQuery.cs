using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Application.Dtos;
namespace VideoGameStore.Application.Games.Query.GetAll
{
    public sealed record GetAllGamesQuery : IQuery<Result<IReadOnlyList<GameDto>>>;
}
