namespace VideoGameStore.Domain.common.Errors
{
    public class RentalErrors
    {
        public static readonly Error LimitExceeded = new("Rental.Limit.Exceeded",
            "Could not rent because you have exceeded your rental limit");
    }
}
