using VideoGameStore.Domain.common;

namespace VideoGameStore.Domain.Entities
{
    public class Manager : BaseEntity, IAggregateRoot
    {
        public string FullName { get; private set; }
        public string Email { get; private set; }
        private Manager() { }

        public Manager(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
        }

        public Game AddGame(string name, string genre, decimal price, DateTime releaseDate)
        {
            return new Game(name, genre, price, releaseDate);
        }

        public void UpdateGamePrice(Game game, decimal newPrice)
        {
            game.UpdatePrice(newPrice);
        }

        public void ToggleGameAvailability(Game game, bool isAvailable)
        {
            if (isAvailable)
                game.MarkAsAvailable();
            else
                game.MarkAsUnavailable();
        }
    }
}
