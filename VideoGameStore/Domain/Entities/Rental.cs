using VideoGameStore.Domain.common;
using VideoGameStore.Domain.Enums;
using VideoGameStore.Domain.Abstractions;
namespace VideoGameStore.Domain.Entities
{
    public class Rental : BaseEntity
    {
        public int GameId { get; private set; }
        public int CustomerId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime? ReturnedAt { get; private set; }
        public RentalStatus Status { get; private set; }

        private Rental() { }

        public Rental(int gameId, int customerId,int days)
        {
            GameId = gameId;
            CustomerId = customerId;
            StartDate = DateTime.UtcNow;
            DueDate = StartDate.AddDays(days);
            Status = RentalStatus.Active;
        }
        public static Result<Rental> Create( Game game , Customer customer , int days)
        {
            if (!game.IsAvailable)
                return Result.Failure<Rental>(new("Game.Unavailable", "Game is not available"));
            if (customer == null)
                return Result.Failure<Rental>(new("Customer.NotFound", "Customer could not be found"));
            var result = new Rental(game.Id , customer.Id,days);
            return Result.Success(result);
        }

        public Result ReturnGame(DateTime now)
        {
            if (ReturnedAt is not null)
                return Result.Failure(new("Rental.IsReturned","Already Returned"));
            Status = RentalStatus.Returned;
            ReturnedAt = now;
            return Result.Success();
        }
    }

}
