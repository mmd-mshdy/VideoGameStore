using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Application.Games.Command.Delete;

public class DeleteGameCommandHandler : ICommandHandler<DeleteGameCommand, string>
{
    private readonly IGenericRepository<Game> _gameRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteGameCommandHandler(IGenericRepository<Game> genericRepository, IUnitOfWork unitOfWork)
    {
        _gameRepository = genericRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
    {
       await _gameRepository.DeleteAsync(request.id);
        await _unitOfWork.CompleteAsync();
        return "Successful";
    }
}
