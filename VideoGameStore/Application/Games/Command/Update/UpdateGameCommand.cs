using MediatR;
using VideoGameStore.Application.Interfaces;

namespace VideoGameStore.Application.Games.Command.Update;

public record UpdateGameCommand(int id ,string Name, string Genre, decimal Price, DateTime ReleaseDate) : ICommand<int>;
