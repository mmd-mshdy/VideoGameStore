using MediatR;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Application.Games.Query.Get;

public record GetGameQuery(int id):IRequest<Game>;

