using VideoGameStore.Domain.Abstractions;
using VideoGameStore.Domain.ValueObjects;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.common.Errors;
using VideoGameStore.Domain.Entities;
namespace VideoGameStore.Domain.Entities
{
    public class Game : BaseEntity , IAggregateRoot
    {
        public string? Name { get;private set; }
        public string? Genre { get; private set; }
        public Money Price { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public bool IsAvailable { get; private set; } = true;
        public Inventory Inventory{ get; private set; }
        private Game() { }
        public Game(string name, string genre, Money price, DateTime releaseDate)
        {
            Name = name;
            Genre = genre;
            Price = price;
            ReleaseDate = releaseDate;
        }
        public void Update(string name, string genre, Money price, DateTime releaseDate)
        {
            Name = name;
            Genre = genre;
            Price = price;
            ReleaseDate = releaseDate;
        }

        public Result UpdatePrice(Money newprice)
        {
            if (newprice.Amount <= 0)
            {
                return Result.Failure(GameErrors.InValidPrice);
            }
            Price = newprice;
            return Result.Success();
        }
        public void MarkAsUnavailable() => IsAvailable = false;
        public void MarkAsAvailable() => IsAvailable = true;
        public void ReserveStock(int quantity)
        {
            Inventory.Reserve(quantity);
        }
        public void ReleaseStock(int quantity)
        {
            Inventory.Release(quantity);
        }
    }
}
