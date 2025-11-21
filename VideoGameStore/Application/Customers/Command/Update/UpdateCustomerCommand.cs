using VideoGameStore.Application.Interfaces;
namespace VideoGameStore.Application.Customers.Command.Update
{
    public record UpdateCustomerCommand(int Id ,string Name , string Email, decimal WalletBalance) : ICommand<int>;
}
