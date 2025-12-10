using MediatR;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Application.Games.Query.Get;

public record GetGameQuery(int id):IRequest<Result<Game>>;

