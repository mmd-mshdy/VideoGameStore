using MediatR;

namespace VideoGameStore.Application.Games.Command.Update;

public record UpdateGameCommand(GameDto GameDto): IRequest<int>;
public record GameDto(string Name, string Genre, decimal Price, DateTime ReleaseDate);