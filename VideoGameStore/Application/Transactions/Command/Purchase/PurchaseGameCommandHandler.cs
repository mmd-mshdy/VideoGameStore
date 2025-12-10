using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.common.Errors;
using VideoGameStore.Domain.Entities;
namespace VideoGameStore.Application.Transactions.Command.Purchase
{
    public class PurchaseGameCommandHandler : ICommandHandler<PurchaseGameCommand, Result<Transaction>>
    {
        private readonly IGenericRepository<Game> _gamesRepository;
        private readonly IGenericRepository<Customer> _customerRepository ;
        private readonly IGenericRepository<Transaction> _transactionRepository ;
        private readonly IUnitOfWork _unitOfWork;
        public PurchaseGameCommandHandler(IGenericRepository<Game> gamesRepository,
            IGenericRepository<Customer> customerRepository ,
            IGenericRepository<Transaction> transactionRepository,
            IUnitOfWork unitOfWork )
        {
            _gamesRepository = gamesRepository;
            _customerRepository = customerRepository;
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Transaction>> Handle(PurchaseGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _gamesRepository.GetByIdAsync(request.gameId);
            var customer = await _customerRepository.GetByIdAsync(request.customerId);
            try
            {
            var transaction =  customer.PurchaseGame(game);
            await _transactionRepository.AddAsync(transaction);
            await _unitOfWork.CompleteAsync();
                return Result.Success(transaction);
            }
            catch
            {
                return Result.Failure<Transaction>(TransactionErrors.TransactionFailed);
            }
        }
    }
}
