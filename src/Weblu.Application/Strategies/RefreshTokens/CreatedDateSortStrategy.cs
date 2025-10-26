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
        public List<RefreshToken> Query(List<RefreshToken> refreshTokens, RefreshTokenParameters refreshTokenParameters)
        {
            if (refreshTokenParameters.CreatedDate == CreatedDateSort.Newest)
            {
                return refreshTokens.OrderByDescending(c => c.CreatedAt).ToList();
            }
            else if (refreshTokenParameters.CreatedDate == CreatedDateSort.Oldest)
            {
                return refreshTokens.OrderBy(c => c.CreatedAt).ToList();
            }
            else
            {
                return refreshTokens;
            }
        }
    }
}