using MediatR;
using Microsoft.AspNetCore.Identity;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Infrastructure.Identity;

namespace VideoGameStore.Application.Login.Query
{
    public sealed class LoginQueryHandler
        : IRequestHandler<LoginQuery, Result<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwt;

        public LoginQueryHandler(
            UserManager<ApplicationUser> userManager,
            IJwtTokenGenerator jwt)
        {
            _userManager = userManager;
            _jwt = jwt;
        }

        public async Task<Result<string>> Handle(
            LoginQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.email);
            if (user is null)
                return Result.Failure<string>(new("AuthErrors.InvalidCredentials","Credentials are invalid"));

            if (!await _userManager.CheckPasswordAsync(user, request.password))
                return Result.Failure<string>(new("AuthErrors.InvalidCredentials", "Credentials are invalid"));

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwt.Generate(user.Id,user.Email!, roles);

            return Result.Success(token);
        }
    }
}
