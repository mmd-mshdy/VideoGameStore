using VideoGameStore.Domain.Abstractions;
using VideoGameStore.Domain.common;
namespace VideoGameStore.Domain.ValueObjects
{
    public sealed record Money(decimal Amount)
    {
        public static readonly Money Zero = new(0);

        public Money Add(Money other)
            => new(Amount + other.Amount);

        public Money Subtract(Money other)
            => new(Amount - other.Amount);

        public Money Multiply(decimal factor)
            => new(Amount * factor);

        public Money ApplyDiscount(Discount discount)
            => discount.IsActive(DateTime.UtcNow)
                ? new Money(discount.Apply(Amount))
                : this;
    }

}
