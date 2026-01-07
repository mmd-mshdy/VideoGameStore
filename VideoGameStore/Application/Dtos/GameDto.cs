namespace VideoGameStore.Application.Dtos;

public sealed record GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    bool IsAvailable
);