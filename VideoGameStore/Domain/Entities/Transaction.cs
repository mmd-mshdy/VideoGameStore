using VideoGameStore.Domain.Abstractions;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.Enums;
using VideoGameStore.Domain.ValueObjects;

namespace VideoGameStore.Domain.Entities;

public class Transaction : BaseEntity
{
    public int CustomerId { get; private set; }
    public int GameId { get; private set; }
    public Money Amount { get; private set; }
    public TransactionType TransactionType { get; private set; }
    public DateTime OccuredAt {  get; private set; }

    protected Transaction() { }

    private Transaction(int customerId, int gameId, Money amount, TransactionType type)
    {
        CustomerId = customerId;
        GameId = gameId;
        Amount = amount;
        TransactionType = type;
        OccuredAt = DateTime.UtcNow;
    }

    public static Transaction CreatePurchase(int customerId, int gameId, Money amount)
        => new(customerId, gameId, amount, TransactionType.Purchase );

    public static Transaction CreateRental(int customerId, int gameId, Money amount)
        => new(customerId, gameId, amount, TransactionType.Rental);
    public static Transaction LateFee(int customerId, int gameId, Money amount)
        => new(customerId, gameId, amount,TransactionType.LateFee);
}
