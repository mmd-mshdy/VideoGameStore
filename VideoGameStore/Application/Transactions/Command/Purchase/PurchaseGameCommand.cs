using VideoGameStore.Application.Interfaces;
namespace VideoGameStore.Application.Transactions.Command.Purchase
{
    public record PurchaseGameCommand(int customerId ,int gameId ) : ICommand<string>;
}
