namespace VideoGameStore.Domain.common.Errors
{
    public static class DiscountErrors
    {
        public static readonly Error InvalidPercentage =
            new("Discount.InvalidPercentage", "Discount percentage must be between 0 and 1.");

        public static readonly Error InvalidDateRange =
            new("Discount.InvalidDateRange", "Discount start date must be before end date.");
    }

}
