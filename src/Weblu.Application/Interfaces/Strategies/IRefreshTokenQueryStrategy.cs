using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Users;

namespace Weblu.Application.Interfaces.Strategies
{
    public interface IRefreshTokenQueryStrategy
    {
        IQueryable<RefreshToken> Query(IQueryable<RefreshToken> refreshTokens, RefreshTokenParameters refreshTokenParameters);

    }
}