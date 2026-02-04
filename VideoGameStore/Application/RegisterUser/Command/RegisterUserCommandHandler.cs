using Microsoft.AspNetCore.Identity;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Infrastructure.Identity;

namespace VideoGameStore.Application.RegisterUser.Command
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = request.email,
                Email = request.email
            };
            var result =await _userManager.CreateAsync(user, request.password);
            if (!result.Succeeded)
                return Result.Failure(new("User.Registration", "User Registration Failed"));
            await _userManager.AddToRoleAsync(user, request.role);
            return Result.Success();
        }
    }
}
