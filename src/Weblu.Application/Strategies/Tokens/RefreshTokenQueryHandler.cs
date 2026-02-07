using Weblu.Application.Interfaces.Strategies.Users;
using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users.Tokens;

namespace Weblu.Application.Strategies.Tokens
{
    public class RefreshTokenQueryHandler
    {
        private IRefreshTokenQueryStrategy _refreshTokenQueryStrategy;
        public RefreshTokenQueryHandler(IRefreshTokenQueryStrategy refreshTokenQueryStrategy)
        {
            _refreshTokenQueryStrategy = refreshTokenQueryStrategy;
        }
        public IQueryable<RefreshToken> ExecuteRefreshTokenQuery(IQueryable<RefreshToken> methods, RefreshTokenParameters refreshTokenParameters)
        {
            return _refreshTokenQueryStrategy.Query(methods, refreshTokenParameters);
        }
    }
}