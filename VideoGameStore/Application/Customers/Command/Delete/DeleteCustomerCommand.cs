using VideoGameStore.Application.Interfaces;

namespace VideoGameStore.Application.Customers.Command.Delete
{
    public record DeleteCustomerCommand(int Id) : ICommand;
}
