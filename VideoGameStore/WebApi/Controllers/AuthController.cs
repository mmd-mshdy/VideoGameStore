using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGameStore.Application.Login.Query;
using VideoGameStore.Application.RegisterUser.Command;

namespace VideoGameStore.WebApi.Controllers
{
    [AllowAnonymous]
    public sealed class AuthController : ApiController
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
            => FromResult(await _sender.Send(command));

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginQuery query)
            => FromResult(await _sender.Send(query));
    }
}
