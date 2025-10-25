using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Users;

namespace Weblu.Application.Strategies.RefreshTokens
{
    public class RefreshTokenQueryHandler
    {
        private IRefreshTokenQueryStrategy _refreshTokenQueryStrategy;
        public RefreshTokenQueryHandler(IRefreshTokenQueryStrategy refreshTokenQueryStrategy)
        {
            _refreshTokenQueryStrategy = refreshTokenQueryStrategy;
        }
        public List<RefreshToken> ExecuteServiceQuery(List<RefreshToken> methods, RefreshTokenParameters refreshTokenParameters)
        {
            return _refreshTokenQueryStrategy.Query(methods, refreshTokenParameters);
        }
    }
}