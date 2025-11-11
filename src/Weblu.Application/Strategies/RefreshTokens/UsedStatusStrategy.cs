using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Users;
using Weblu.Domain.Enums.Tokens;

namespace Weblu.Application.Strategies.RefreshTokens
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