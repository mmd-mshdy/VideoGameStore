
namespace VideoGameStore.Domain.common.Errors
{
    public static class TransactionErrors
    {
        public static readonly Error TransactionFailed = new("Transaction.Failed", "Couldn't finidh the transaction");
    }
}
