using Weblu.Application.Interfaces.Strategies.Users;
using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users;
using Weblu.Domain.Entities.Users.Tokens;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Tokens
{
    public class CreatedDateSortStrategy : IRefreshTokenQueryStrategy
    {
        public IQueryable<RefreshToken> Query(IQueryable<RefreshToken> refreshTokens, RefreshTokenParameters refreshTokenParameters)
        {
            if (refreshTokenParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return refreshTokens.OrderByDescending(c => c.CreatedAt);
            }
            else if (refreshTokenParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return refreshTokens.OrderBy(c => c.CreatedAt);
            }
            else
            {
                return refreshTokens;
            }
        }
    }
}