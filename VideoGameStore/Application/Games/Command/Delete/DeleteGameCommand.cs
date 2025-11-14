using MediatR;

namespace VideoGameStore.Application.Games.Command.Delete;

public record DeleteGameCommand(int id):IRequest<string>;
