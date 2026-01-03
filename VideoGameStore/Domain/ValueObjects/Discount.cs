using VideoGameStore.Domain.common;
using VideoGameStore.Domain.common.Errors;
using VideoGameStore.Domain.Abstractions;
namespace VideoGameStore.Domain.ValueObjects
{

    public sealed class Discount : ValueObject
    {
        public decimal Percentage { get; }
        public DateTime StartsAt { get; }
        public DateTime EndsAt { get; }

        private Discount(decimal percentage, DateTime startsAt, DateTime endsAt)
        {
            Percentage = percentage;
            StartsAt = startsAt;
            EndsAt = endsAt;
        }

        public static Result<Discount> Create(
            decimal percentage,
            DateTime startsAt,
            DateTime endsAt)
        {
            if (percentage <= 0 || percentage >= 1)
                return Result.Failure<Discount>(DiscountErrors.InvalidPercentage);

            if (startsAt >= endsAt)
                return Result.Failure<Discount>(DiscountErrors.InvalidDateRange);

            return Result.Success(new Discount(percentage, startsAt, endsAt));
        }

        public override IEnumerable<object> GetAtomicValue()
        {
            yield return Percentage;
            yield return StartsAt;
            yield return EndsAt;
        }
        public bool IsActive(DateTime now) => now >= StartsAt && now <= EndsAt;
        public decimal Apply(decimal price) => price - (price * Percentage);
    }


}
