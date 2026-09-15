using ArenaHub.API.Entities;

namespace ArenaHub.API.Services
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}
