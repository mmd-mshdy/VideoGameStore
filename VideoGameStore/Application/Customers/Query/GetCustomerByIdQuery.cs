using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;

namespace VideoGameStore.Application.Customers.Query
{
    public sealed record GetCustomerByIdQuery(int CustomerId) : IQuery<Result<CustomerResponse>>;
    public sealed record CustomerResponse(int CustomerId , string Email );

}
