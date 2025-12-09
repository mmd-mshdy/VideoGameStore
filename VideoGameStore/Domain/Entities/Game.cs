using VideoGameStore.Domain.Abstractions;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.common.Errors;
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

        public Result UpdatePrice(decimal newprice)
        {
            if (newprice <= 0)
            {
                return Result.Failure(GameErrors.InValidPrice);
            }
            Price = newprice;
            return Result.Success();
        }
        public void MarkAsUnavailable() => IsAvailable = false;
        public void MarkAsAvailable() => IsAvailable = true;
    }
}
