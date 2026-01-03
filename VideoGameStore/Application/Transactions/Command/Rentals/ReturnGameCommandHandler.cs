using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;

namespace VideoGameStore.Application.Transactions.Command.Rentals
{
    public class ReturnGameCommandHandler : ICommandHandler<ReturnGameCommand, Result<Domain.Entities.Rental>>
    {
        private readonly IRentalRepository _rentals;
        private readonly IUnitOfWork _unitOfWork;
        public ReturnGameCommandHandler(IRentalRepository rental , IUnitOfWork unitOfWork)
        {
            _rentals = rental;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<Domain.Entities.Rental>> Handle(ReturnGameCommand request, CancellationToken cancellationToken)
        {
            var rental =await _rentals.GetByIdAsync(request.id);
            if (rental == null)
            {
                return Result.Failure<Domain.Entities.Rental>(new("Rental.NotFound", "Rental Not Fouund"));
            }
            rental.ReturnGame(DateTime.Now);

            return Result.Success(rental);
        }
    }
}
