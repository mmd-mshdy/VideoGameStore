using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.common.Errors;
using VideoGameStore.Domain.Entities;
using VideoGameStore.Domain.ValueObjects;
namespace VideoGameStore.Application.Transactions.Command.Rental
{
    public class RentalGameCommandHandler : ICommandHandler<RentalGameCommand, Result<Domain.Entities.Rental>>
    {
        private readonly IRentalRepository _rentals;
        private readonly IGenericRepository<Customer> _customers;
        private readonly IGenericRepository<Game> _games;
        private readonly IUnitOfWork _unitOfWork;
        public RentalGameCommandHandler(IGenericRepository<Game> games,
            IGenericRepository<Customer> customers,
            IRentalRepository rentals,
            IUnitOfWork unitOfWork)
        {
            _games = games;
            _customers = customers;
            _rentals = rentals;
            _unitOfWork = unitOfWork;

        }
        public async Task<Result<Domain.Entities.Rental>> Handle(RentalGameCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customers.GetByIdAsync(request.customerId);
            var game= await _games.GetByIdAsync(request.gameId);
            try
            {
                var rentfee = new Money(request.rentPrice);
                var rental = new Domain.Entities.Rental(game.Id, customer.Id, request.days);
                await _rentals.Add(rental);
                await _unitOfWork.CompleteAsync();
                return Result.Success(rental);
            }
            catch
            {
                return Result.Failure<Domain.Entities.Rental>(TransactionErrors.TransactionFailed);
            }
        }
    }
}
