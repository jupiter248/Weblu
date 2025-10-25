using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Users;

namespace Weblu.Application.Strategies.RefreshTokens
{
    public class FilterByUserIdStrategy : IRefreshTokenQueryStrategy
    {
        public List<RefreshToken> Query(List<RefreshToken> refreshTokens, RefreshTokenParameters refreshTokenParameters)
        {
            if (!string.IsNullOrEmpty(refreshTokenParameters.FilterByUserId))
            {
                return refreshTokens.Where(u => u.UserId == refreshTokenParameters.FilterByUserId).ToList();
            }
            else
            {
                return refreshTokens;
            }
        }
    }
}