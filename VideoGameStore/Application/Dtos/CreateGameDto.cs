namespace VideoGameStore.Application.Dtos
{
    public record CreateGameDto(string Name ,string Genre , Decimal Price , DateTime ReleaseDate);
    
}
