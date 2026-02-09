using Weblu.Application.Interfaces.Strategies.Users;
using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users.Tokens;
using Weblu.Domain.Enums.Users.Tokens;

namespace Weblu.Application.Strategies.Tokens
{
    public class UsedStatusStrategy : IRefreshTokenQueryStrategy
    {
        public IQueryable<RefreshToken> Query(IQueryable<RefreshToken> refreshTokens, RefreshTokenParameters refreshTokenParameters)
        {
            if (refreshTokenParameters.UsedStatus == UsedStatus.Used)
            {
                return refreshTokens.Where(u => u.IsUsed == true);
            }
            else if (refreshTokenParameters.UsedStatus == UsedStatus.Unused)
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