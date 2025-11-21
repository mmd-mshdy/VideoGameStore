using VideoGameStore.Application.Interfaces;

namespace VideoGameStore.Application.Customers.Command.Create
{
    public record CreateCustomerCommand(string Name, string Email) : ICommand<string>;
}
