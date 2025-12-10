using MediatR;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.common.Errors;
using VideoGameStore.Domain.Entities;
namespace VideoGameStore.Application.Customers.Query
{
    internal sealed class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, Result<CustomerResponse>>
    {
        private readonly IGenericRepository<Customer> _repository;
        public GetCustomerByIdQueryHandler(IGenericRepository<Customer> repository)
        {
            _repository = repository;
        }
        

         async Task<Result<CustomerResponse>> IRequestHandler<GetCustomerByIdQuery, Result<CustomerResponse>>.Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {

            var customer = await _repository.GetByIdAsync(request.CustomerId );
            if (customer != null)
            {
                var response = new CustomerResponse(customer.Id, customer.Email);
                return response;
            }
            return Result.Failure<CustomerResponse>(new Error("Customer.Response.NotFound",
                "Failed To fetch Customer"));
        }
    }
}
