namespace VideoGameStore.Application.Dtos
{
    public record UpdateGameDto(string Name, string Genre, Decimal Price, DateTime ReleaseDate);

}
