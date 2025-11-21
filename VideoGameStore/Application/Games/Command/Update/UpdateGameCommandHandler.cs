using MediatR;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Application.Games.Command.Update
{
    public class UpdateGameCommandHandler : ICommandHandler<UpdateGameCommand, int>
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
            var game = await _gameRepository.GetByIdAsync(request.id);
            if (game == null)
            {
                return 0;
            }
            game.Update(request.Name, request.Genre, request.Price, request.ReleaseDate);
            await _gameRepository.UpdateAsync(game);
            await _unitOfWork.CompleteAsync();
            return game.Id;
        }
    }
}
