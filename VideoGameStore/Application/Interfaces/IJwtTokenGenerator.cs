using VideoGameStore.Infrastructure.Identity;

namespace VideoGameStore.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        public string Generate(int userId,string email, IList<string> roles);
    }
}
