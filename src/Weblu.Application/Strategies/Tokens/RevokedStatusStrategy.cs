using Weblu.Application.Interfaces.Strategies.Users;
using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users;
using Weblu.Domain.Entities.Users.Tokens;
using Weblu.Domain.Enums.Users;
using Weblu.Domain.Enums.Users.Tokens;

namespace Weblu.Application.Strategies.Tokens
{
    public class RevokedStatusStrategy : IRefreshTokenQueryStrategy
    {
        public IQueryable<RefreshToken> Query(IQueryable<RefreshToken> refreshTokens, RefreshTokenParameters refreshTokenParameters)
        {
            if (refreshTokenParameters.RevokedStatus == RevokedStatus.Revoked)
            {
                return refreshTokens.Where(u => u.IsRevoked == true);
            }
            else if (refreshTokenParameters.RevokedStatus == RevokedStatus.Active)
            {
                return refreshTokens.Where(u => u.IsUsed == false);
            }
            else
            {
                return refreshTokens;
            }
        }
    }
}