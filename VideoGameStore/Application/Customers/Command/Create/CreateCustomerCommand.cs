using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Application.Customers.Command.Create
{
    public record CreateCustomerCommand(string Name, string Email) : ICommand<Result<Customer>> ;
}
