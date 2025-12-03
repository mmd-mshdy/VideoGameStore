using Microsoft.CodeAnalysis.Operations;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.Enums;

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
        public void AddBalance (decimal amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Amount should be positive");
            WalletBalance += amount;
        }
        public void Update(string name , string email , decimal walletBallance=0)
        {
            name = Name;
            email = Email;
            walletBallance = WalletBalance;
        }
        public void DeductBalance(decimal amount)
        {
            if (amount > WalletBalance)
                throw new InvalidOperationException("Insufficient Balance");
            WalletBalance -= amount;
        }
        public Transaction PurchaseGame(Game game)
        {
            if (!game.IsAvailable)
                throw new InvalidOperationException("Game not available");
            DeductBalance(game.Price);
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
