using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.Entities;
namespace VideoGameStore.Application.Customers.Command.Update
{
    public record UpdateCustomerCommand(int Id ,string Name , string Email) : ICommand<Result<Customer>>;
}
