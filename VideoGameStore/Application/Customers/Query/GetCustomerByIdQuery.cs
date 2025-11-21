using VideoGameStore.Application.Interfaces;

namespace VideoGameStore.Application.Customers.Query
{
    public sealed record GetCustomerByIdQuery(int CustomerId) : IQuery<CustomerResponse>;
    public sealed record CustomerResponse(int CustomerId , string Email );

}
