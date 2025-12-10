using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.Entities;
namespace VideoGameStore.Application.Transactions.Command.Purchase
{
    public record PurchaseGameCommand(int customerId ,int gameId ) : ICommand<Result<Transaction>>;
}
