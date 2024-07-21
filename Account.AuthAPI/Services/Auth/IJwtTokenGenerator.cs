using Account.AuthAPI.Models.Auth;

namespace Account.AuthAPI.Service.Auth
{
    public interface IJwtTokenGenerator
    {
        Task<Tuple<string, int>> GenerateToken(ApplicationUser applicationUser);
    }
}
