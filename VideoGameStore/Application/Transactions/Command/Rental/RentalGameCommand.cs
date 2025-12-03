using VideoGameStore.Application.Interfaces;
namespace VideoGameStore.Application.Transactions.Command.Rental
{
    public record RentalGameCommand(int gameId , int customerId , int rentPrice) : ICommand<string>;
}
