using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Application.Customers.Command.Update
{
    public class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand, int>
    {
        private readonly IGenericRepository<Customer> _repository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCustomerCommandHandler(IGenericRepository<Customer> repository , IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
                
        }
        public async Task<int> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(request.Id);
            if (customer != null)
            {
                await _repository.UpdateAsync(customer);
                await _unitOfWork.CompleteAsync();
                return customer.Id;
            }
            return 0;
        }
    }
}
