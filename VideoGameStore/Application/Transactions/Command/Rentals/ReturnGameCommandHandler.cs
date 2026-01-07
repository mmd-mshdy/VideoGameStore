using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Application.Transactions.Command.Rentals
{
    public class ReturnGameCommandHandler : ICommandHandler<ReturnGameCommand, Result<Domain.Entities.Rental>>
    {
        private readonly IRentalRepository _rentals;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Transaction> _transactions;
        public ReturnGameCommandHandler(IRentalRepository rental ,IGenericRepository<Transaction> transaction ,IUnitOfWork unitOfWork)
        {
            _rentals = rental;
            _transactions = transaction;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Domain.Entities.Rental>> Handle(ReturnGameCommand request, CancellationToken cancellationToken)
        {
            var rental =await _rentals.GetByIdAsync(request.id);
            if (rental == null)
            {
                return Result.Failure<Domain.Entities.Rental>(new("Rental.NotFound", "Rental Not Fouund"));
            }
            var rentalResult =rental.Return(DateTime.Now);
            var lateFee = rentalResult.Value;
            if (lateFee.Amount > 0)
            {
                var transaction = Transaction.LateFee(
                    rental.CustomerId,
                    rental.GameId,
                    lateFee
                    );
                await _transactions.AddAsync(transaction);
            }
            await _unitOfWork.CompleteAsync();
            return Result.Success(rental);
        }
    }
}
