using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Application.Customers.Command.Create
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Customer> _genericRepository;
        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork , IGenericRepository<Customer> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
              
        }
        public async Task<string> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(request.Name, request.Email);
            if (customer != null)
            {
                await _genericRepository.AddAsync(customer);
                return customer.Email;
            }
            return "Invalid Customer";

        }
    }
}
