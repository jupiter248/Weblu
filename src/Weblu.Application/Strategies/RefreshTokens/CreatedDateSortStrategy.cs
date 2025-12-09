using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Users;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.RefreshTokens
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