using VideoGameStore.Domain.common;
using VideoGameStore.Domain.Abstractions;
using VideoGameStore.Domain.Enums;
using VideoGameStore.Domain.common.Errors;

namespace VideoGameStore.Domain.Entities
{
    public class Customer :BaseEntity , IAggregateRoot
    {
        public string? Name { get; private set; } = null;
        public string? Email { get; private set; } = null;
        public decimal WalletBalance { get; private set; } = 0;
        public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();
        private Customer() { }
        public Customer(string name , string email , decimal walletbalance=0) 
        {
            Name = name;
            Email = email;
            WalletBalance = walletbalance;
        }
        public Result AddBalance (decimal amount)
        {
            if (amount <= 0)
                return Result.Failure(CustomerErrors.InvalidAmountToAdd);
            WalletBalance += amount;
            return Result.Success();
        }
        public void Update(string name , string email , decimal walletBallance=0)
        {
            name = Name;
            email = Email;
            walletBallance = WalletBalance;
        }
        public Result DeductBalance(decimal amount)
        {
            if (amount > WalletBalance)
                return Result.Failure(CustomerErrors.InsufficientBalance);
            WalletBalance -= amount;

            return Result.Success();
        }
        public Result<Transaction> PurchaseGame(Game game)
        {
            if (!game.IsAvailable)
                return GameErrors.GameUnavailable;
            var balanceresult = DeductBalance(game.Price);
            if (balanceresult.IsFailure) return CustomerErrors.InsufficientBalance;
            var purchase = new Transaction(game, this, game.Price, TransactionType.Purchase);
            Transactions.Add(purchase); 
            return purchase;
        }
        public Transaction RentGame(Game game, decimal rentprice)
        {
            if (!game.IsAvailable)
                throw new InvalidOperationException("Game not available");
            DeductBalance(rentprice);
            var rental = new Transaction(game, this, rentprice, TransactionType.Rent);
            Transactions.Add(rental);
            return rental;
        }
    }
}
