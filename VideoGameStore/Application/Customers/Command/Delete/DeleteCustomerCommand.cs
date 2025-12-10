using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Application.Customers.Command.Delete
{
    public record DeleteCustomerCommand(int Id) : ICommand<Result<Customer>>;
}
