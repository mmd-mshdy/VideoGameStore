using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.common.Errors;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Application.Customers.Command.Delete
{
    public class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand, Result<Customer>>
    {
        private readonly IGenericRepository<Customer> _repository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCustomerCommandHandler(IGenericRepository<Customer> repository , IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Customer>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer =await _repository.GetByIdAsync(request.Id);
            if (customer != null)
            {
                await _repository.DeleteAsync(request.Id);
                await _unitOfWork.CompleteAsync();
                return Result.Success(customer);
            }
            return Result.Failure<Customer>(CustomerErrors.FailedToFetchCustomer); 
            

        }
    }
}
