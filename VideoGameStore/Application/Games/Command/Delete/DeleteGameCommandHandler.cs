using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.common.Errors;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Application.Games.Command.Delete;

public class DeleteGameCommandHandler : ICommandHandler<DeleteGameCommand, Result<Game>>
{
    private readonly IGenericRepository<Game> _gameRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteGameCommandHandler(IGenericRepository<Game> genericRepository, IUnitOfWork unitOfWork)
    {
        _gameRepository = genericRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Game>> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
    {
        var game = await _gameRepository.GetByIdAsync(request.id);
        if (game != null)
        {
            await _gameRepository.DeleteAsync(request.id);
            await _unitOfWork.CompleteAsync();
            return Result.Success(game);
        }
        return Result.Failure<Game>(GameErrors.GameNotFetched);
    }
}
