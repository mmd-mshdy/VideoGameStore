using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.Entities;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.common.Errors;

namespace VideoGameStore.Application.Customers.Command.Create
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, Result<Customer>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Customer> _genericRepository;
        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork , IGenericRepository<Customer> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
              
        }
        public async Task<Result<Customer>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(request.Name, request.Email);
            if (customer != null)
            {
                await _genericRepository.AddAsync(customer);
                await _unitOfWork.CompleteAsync();
                return Result.Success(customer);
            }
            return Result.Failure<Customer>(CustomerErrors.AddingCustomerFailed);

        }
    }
}
