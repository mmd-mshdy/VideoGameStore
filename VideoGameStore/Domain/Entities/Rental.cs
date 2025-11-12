namespace VideoGameStore.Domain.Entities
{
    public class Rental
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; } = default;
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; } = default;
    }
}
