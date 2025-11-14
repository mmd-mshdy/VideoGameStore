using MediatR;
using NuGet.Protocol.Plugins;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Application.Games.Command.Create
{
    public class CreateGameHandler : IRequestHandler<CreateGameCommand , int>
    {
        private readonly IGenericRepository<Game> _gameRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateGameHandler(IGenericRepository<Game> genericRepository , IUnitOfWork unitOfWork)
        {
                _gameRepository = genericRepository;
                _unitOfWork = unitOfWork;
        }

        async Task<int> IRequestHandler<CreateGameCommand, int>.Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var game = new Game(request.Name, request.Genre, request.Price, request.ReleaseDate);
            await _gameRepository.AddAsync(game);
            await _unitOfWork.CompleteAsync();
            return game.Id;
        }
    }
}
