using VideoGameStore.Domain.common;
using VideoGameStore.Domain.Enums;

namespace VideoGameStore.Domain.Entities
{
    public class Transaction :BaseEntity
    {
        public int GameId { get; private set; }
        public Game? Game { get; private set; } = default;
        public int CustomerId { get; private set; }
        public Customer? Customer { get; private set; } = default;
        public decimal Amount { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public TransactionType TransactionType { get; private set; }
        private Transaction() { }
        public Transaction( Game game , Customer customer, decimal amount,TransactionType transactiontype )
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Game = game ?? throw new ArgumentNullException(nameof(game));
            Amount = amount;
            TransactionType = transactiontype;
            TransactionDate = DateTime.UtcNow;
        }
        public bool IsRental() => TransactionType == TransactionType.Rent;
        public bool IsPurchase() => TransactionType == TransactionType.Purchase;

    }
}
