// VideoGameStore.Application/Validators/CreateGameCommandValidator.cs
using FluentValidation;

namespace VideoGameStore.Application.Games.Command.Create
{
    public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
    {
        public CreateGameCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Genre).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.ReleaseDate).LessThanOrEqualTo(System.DateTime.UtcNow).WithMessage("Release date cannot be in the future.");
        }
    }
}
