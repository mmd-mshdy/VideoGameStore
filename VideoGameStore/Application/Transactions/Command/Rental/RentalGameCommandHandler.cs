using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.common.Errors;
using VideoGameStore.Domain.Entities;
namespace VideoGameStore.Application.Transactions.Command.Rental
{
    public class RentalGameCommandHandler : ICommandHandler<RentalGameCommand, Result<Transaction>>
    {
        private readonly IGenericRepository<Transaction> _transactionRepository;
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IGenericRepository<Game> _gameRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RentalGameCommandHandler(IGenericRepository<Game> gamesRepository,
            IGenericRepository<Customer> customerRepository,
            IGenericRepository<Transaction> transactionRepository,
            IUnitOfWork unitOfWork)
        {
            _gameRepository = gamesRepository;
            _customerRepository = customerRepository;
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;

        }
        public async Task<Result<Transaction>> Handle(RentalGameCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.customerId);
            var game= await _gameRepository.GetByIdAsync(request.gameId);
            try
            {
                var rental = customer.RentGame(game, request.rentPrice);
                await _transactionRepository.AddAsync(rental);
                await _unitOfWork.CompleteAsync();
                return Result.Success(rental);
            }
            catch
            {
                return Result.Failure<Transaction>(TransactionErrors.TransactionFailed);
            }
        }
    }
}
