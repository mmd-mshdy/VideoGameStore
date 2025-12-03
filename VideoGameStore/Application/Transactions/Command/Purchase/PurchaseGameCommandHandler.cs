using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.Entities;
namespace VideoGameStore.Application.Transactions.Command.Purchase
{
    public class PurchaseGameCommandHandler : ICommandHandler<PurchaseGameCommand, string>
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

        public async Task<string> Handle(PurchaseGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _gamesRepository.GetByIdAsync(request.gameId);
            if (game == null) return "Game not found";
            var customer = await _customerRepository.GetByIdAsync(request.customerId);
            if (customer == null) return "csustomer not found";

            try
            {
            var transaction =  customer.PurchaseGame(game);
            await _transactionRepository.AddAsync(transaction);
            await _unitOfWork.CompleteAsync();
            }
            catch(InvalidOperationException ex)
            {
                return ex.Message;
            }
            catch(Exception ex)
            {
                return $"{ex.Message}";
            }
            return "Purchased";
        }
    }
}
