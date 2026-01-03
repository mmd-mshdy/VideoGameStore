using MediatR;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.common.Errors;
using VideoGameStore.Domain.Entities;
using VideoGameStore.Domain.ValueObjects;

namespace VideoGameStore.Application.Games.Command.Create
{
    public class CreateGameHandler : ICommandHandler<CreateGameCommand , Result<Game>>
    {
        private readonly IGenericRepository<Game> _gameRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateGameHandler(IGenericRepository<Game> genericRepository , IUnitOfWork unitOfWork)
        {
                _gameRepository = genericRepository;
                _unitOfWork = unitOfWork;
        }

        async Task<Result<Game>> IRequestHandler<CreateGameCommand, Result<Game>>.Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var price = new Money(request.Price);
                var game = new Game(request.Name, request.Genre, price, request.ReleaseDate);
                await _gameRepository.AddAsync(game);
                await _unitOfWork.CompleteAsync();
                return Result.Success(game);
            }
            catch
            {
                return Result.Failure<Game>(GameErrors.GameUnavailable);
            }
        }
    }
}
