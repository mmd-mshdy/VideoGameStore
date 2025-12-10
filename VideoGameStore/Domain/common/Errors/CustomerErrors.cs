namespace VideoGameStore.Domain.common.Errors
{
    public static class CustomerErrors
    {
        public static readonly Error InvalidAmountToAdd =
            new("Customer.AddBalance.Amount.Invalid",
            "The amount to add is below zero");
        public static readonly Error InsufficientBalance =
            new("customer.DeductBalance.InsufficientBalance",
                "Balance not enough ");
        public static readonly Error AddingCustomerFailed =
            new("Customer.Add.Failure",
            "Failed to Add customer");
        public static readonly Error FailedToFetchCustomer =
            new("Customer.Find.Failed",
            "Failed to Fetch customer");


    }
}
