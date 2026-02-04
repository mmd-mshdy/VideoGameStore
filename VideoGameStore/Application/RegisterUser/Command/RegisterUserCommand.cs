
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;

namespace VideoGameStore.Application.RegisterUser.Command
{
    public sealed record RegisterUserCommand(string email, string password , string role) : ICommand<Result>;
}
