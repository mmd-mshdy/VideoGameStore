using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;

namespace VideoGameStore.Application.Login.Query
{
    public sealed record LoginQuery(string email , string password) : ICommand<Result<string>>;
}
