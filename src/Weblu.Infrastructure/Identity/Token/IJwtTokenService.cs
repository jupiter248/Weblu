using   Weblu.Infrastructure.Identity.Entities;

namespace Weblu.Infrastructure.Identity.Token
{
    public interface IJwtTokenService
    {
        public string GenerateAccessToken(AppUser user, IList<string> roles , IList<string> roleClaims);
        public string GenerateRefreshToken();
    }
}