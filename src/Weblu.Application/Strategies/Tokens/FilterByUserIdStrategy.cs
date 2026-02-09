using Weblu.Application.Interfaces.Strategies.Users;
using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users.Tokens;

namespace Weblu.Application.Strategies.Tokens
{
    public class FilterByUserIdStrategy : IRefreshTokenQueryStrategy
    {
        public IQueryable<RefreshToken> Query(IQueryable<RefreshToken> refreshTokens, RefreshTokenParameters refreshTokenParameters)
        {
            return refreshTokens.Where(u => u.UserId == refreshTokenParameters.FilterByUserId);
        }
    }
}