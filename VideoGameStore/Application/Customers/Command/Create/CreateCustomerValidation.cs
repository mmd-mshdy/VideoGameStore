
using FluentValidation;

namespace VideoGameStore.Application.Customers.Command.Create
{
    internal class CreateCustomerValidation : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidation() 
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}
