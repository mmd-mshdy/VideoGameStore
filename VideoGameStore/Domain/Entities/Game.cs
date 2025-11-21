using VideoGameStore.Domain.common;
namespace VideoGameStore.Domain.Entities
{
    public class Game : BaseEntity , IAggregateRoot
    {
        public string? Name { get;private set; }
        public string? Genre { get; private set; }
        public decimal Price { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public bool IsAvailable { get; private set; } = true;
        public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();
        private Game() { }
        public Game(string name, string genre, decimal price, DateTime releaseDate)
        {
            Name = name;
            Genre = genre;
            Price = price;
            ReleaseDate = releaseDate;
        }
        public void Update(string name, string genre, decimal price, DateTime releaseDate)
        {
            Name = name;
            Genre = genre;
            Price = price;
            ReleaseDate = releaseDate;
        }

        public void UpdatePrice(decimal newprice)
        {
            if (newprice <= 0)
            {
                throw new InvalidOperationException("Price must be above 0 ");
            }
            Price = newprice;

        }
        public void MarkAsUnavailable() => IsAvailable = false;
        public void MarkAsAvailable() => IsAvailable = true;
    }
}
