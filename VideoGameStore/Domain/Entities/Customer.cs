using Microsoft.CodeAnalysis.Operations;
using VideoGameStore.Domain.common;

namespace VideoGameStore.Domain.Entities
{
    public class Customer :BaseEntity , IAggregateRoot
    {
        public string? Name { get; private set; } = null;
        public string? Email { get; private set; } = null;
        public decimal WalletBalance { get; private set; }
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
        public void DeductBalance(decimal amount)
        {
            if (amount > WalletBalance)
                throw new InvalidOperationException("Insufficient Balance");
            WalletBalance -= amount;
        }
        public void PurchaseGame(Game game)
        {
            if (!game.IsAvailable)
                throw new InvalidOperationException("Game not available");
            DeductBalance(game.Price);
            Transactions.Add(new Transaction(game,this,game.Price,Enums.TransactionType.Purchase));
        }
        public void RentGame(Game game, decimal rentprice)
        {
            if (!game.IsAvailable)
                throw new InvalidOperationException("Game not available");
            DeductBalance(rentprice);
            Transactions.Add(new Transaction(game, this, rentprice, Enums.TransactionType.Purchase));
        }
    }
}
