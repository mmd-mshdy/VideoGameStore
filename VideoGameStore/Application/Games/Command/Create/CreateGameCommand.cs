using VideoGameStore.Application.Interfaces;
namespace VideoGameStore.Application.Games.Command.Create
{
    public record CreateGameCommand(string Name ,string Genre , decimal Price , DateTime ReleaseDate): ICommand<int>;
    
}
