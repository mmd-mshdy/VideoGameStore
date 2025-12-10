using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.common.Errors;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Application.Customers.Command.Update
{
    public class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand, Result<Customer>>
    {
        private readonly IGenericRepository<Customer> _repository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCustomerCommandHandler(IGenericRepository<Customer> repository , IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
                
        }
        public async Task<Result<Customer>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(request.Id);
            if (customer != null)
            {
                customer.Update(request.Name, request.Email);
                await _repository.UpdateAsync(customer);
                await _unitOfWork.CompleteAsync();
                return Result.Success(customer) ;
            }
            return Result.Failure<Customer>(CustomerErrors.FailedToFetchCustomer);
        }
    }
}
