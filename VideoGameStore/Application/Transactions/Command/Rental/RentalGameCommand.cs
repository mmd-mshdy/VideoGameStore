using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.Entities;
namespace VideoGameStore.Application.Transactions.Command.Rental
{
    public record RentalGameCommand(int gameId , int customerId , int rentPrice) : ICommand<Result<Transaction>>;
}
