namespace VideoGameStore.Application.Games.Command.Update
{
    public record UpdateGameCommand(string Name, string Genre, decimal Price, DateTime ReleaseDate);

}
