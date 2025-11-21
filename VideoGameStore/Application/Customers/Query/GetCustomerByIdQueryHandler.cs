using MediatR;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.Entities;
namespace VideoGameStore.Application.Customers.Query
{
    internal sealed class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, CustomerResponse>
    {
        private readonly IGenericRepository<Customer> _repository;
        public GetCustomerByIdQueryHandler(IGenericRepository<Customer> repository)
        {
            _repository = repository;
        }
        

         async Task<CustomerResponse> IRequestHandler<GetCustomerByIdQuery, CustomerResponse>.Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(request.CustomerId );
            var response =  new CustomerResponse(customer.Id, customer.Email);
            return response;
        }
    }
}
