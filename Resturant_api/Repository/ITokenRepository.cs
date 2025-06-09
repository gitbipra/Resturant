using Microsoft.AspNetCore.Identity;

namespace Resturant_api.Repository
{
    public interface ITokenRepository
    {
       string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
