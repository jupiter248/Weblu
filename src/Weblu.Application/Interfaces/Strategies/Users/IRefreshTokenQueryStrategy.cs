using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users.Tokens;

namespace Weblu.Application.Interfaces.Strategies.Users
{
    public interface IRefreshTokenQueryStrategy
    {
        IQueryable<RefreshToken> Query(IQueryable<RefreshToken> refreshTokens, RefreshTokenParameters refreshTokenParameters);

    }
}