using VideoGameStore.Domain.common;
using VideoGameStore.Domain.Abstractions;
using VideoGameStore.Domain.Enums;
using VideoGameStore.Domain.common.Errors;
using VideoGameStore.Domain.ValueObjects;

namespace VideoGameStore.Domain.Entities
{

    public class Customer : BaseEntity, IAggregateRoot
    {
        private readonly List<Rental> _rentals = new();
        private readonly List<Transaction> _transactions = new();

        public string Name { get; private set; }
        public Money WalletBalance { get; private set; }
        public Membership Membership { get; private set; }

        public IReadOnlyCollection<Rental> Rentals => _rentals;
        public IReadOnlyCollection<Transaction> Transactions => _transactions;

        protected Customer() { }

        public Customer(string name, Money initialBalance, Membership membership)
        {
            Name = name;
            WalletBalance = initialBalance;
            Membership = membership;
        }

        public Result<Rental> RentGame(Game game, Money rentPrice)
        {
            if (!Membership.CanRent(_rentals.Count(r => r.Status == RentalStatus.Active)))
                return Result.Failure<Rental>(RentalErrors.LimitExceeded);

            WalletBalance = WalletBalance.Subtract(rentPrice);
            game.ReserveStock(1);

            var rental = new Rental(game.Id, Id);
            _rentals.Add(rental);

            _transactions.Add(Transaction.CreateRental(Id, game.Id, rentPrice));
            return rental;
        }

        public Transaction PurchaseGame(Game game)
        {
            WalletBalance = WalletBalance.Subtract(game.Price);
            game.ReserveStock(1);

            var transaction = Transaction.CreatePurchase(Id, game.Id, game.Price);
            _transactions.Add(transaction);

            return transaction;
        }
    }
}
