using MediatR;
using NuGet.Protocol.Plugins;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.common.Errors;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Application.Games.Query.Get;

public class GetGameQueryHandler : IRequestHandler<GetGameQuery, Result<Game>>
{
    private readonly IGenericRepository<Game> _gameRepository;
    private readonly IUnitOfWork _unitOfWork;
    public GetGameQueryHandler(IGenericRepository<Game> genericRepository, IUnitOfWork unitOfWork)
    {
        _gameRepository = genericRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Game>> Handle(GetGameQuery request, CancellationToken cancellationToken)
    {
       var game=await _gameRepository.GetByIdAsync(request.id);
        if (game is null) return Result.Failure<Game>(GameErrors.GameNotFetched);
        return game;
    }
}