namespace VideoGameStore.Application.Dtos
{
    public record class GameDto(
        string Name,
        string Genre,
        decimal Price,
        DateTime ReleaseDate
    );
}
