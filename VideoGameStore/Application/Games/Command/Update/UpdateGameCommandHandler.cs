using MediatR;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Application.Games.Command.Update
{
    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, int>
    {
        private readonly IGenericRepository<Game> _gameRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateGameCommandHandler(IGenericRepository<Game> genericRepository, IUnitOfWork unitOfWork)
        {
            _gameRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            var game = new Game(request.GameDto.Name, request.GameDto.Genre, request.GameDto.Price, request.GameDto.ReleaseDate);
            await _gameRepository.UpdateAsync(game);
            await _unitOfWork.CompleteAsync();
            return game.Id;
        }
    }
}
