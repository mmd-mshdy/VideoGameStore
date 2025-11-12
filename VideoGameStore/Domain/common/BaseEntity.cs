namespace VideoGameStore.Domain.common
{
    public abstract class BaseEntity
    {
        public int  Id { get;protected set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public void MarkUpdate() => UpdatedAt = DateTime.UtcNow;
    }
}
