using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Application.Games.Command.Create
{
    public record CreateGameCommand(string Name ,string Genre , decimal Price , DateTime ReleaseDate): ICommand<Result<Game>>;
    
}
