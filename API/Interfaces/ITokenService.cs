
using API.Entities;

namespace API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);

        string CreateAdminToken(AppUser user);
    }
}