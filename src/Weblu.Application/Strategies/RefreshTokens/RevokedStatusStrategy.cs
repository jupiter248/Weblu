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
    public class RevokedStatusStrategy : IRefreshTokenQueryStrategy
    {
        public List<RefreshToken> Query(List<RefreshToken> refreshTokens, RefreshTokenParameters refreshTokenParameters)
        {
            if (refreshTokenParameters.RevokedStatus == RevokedStatus.Revoked)
            {
                return refreshTokens.Where(u => u.IsRevoked == true).ToList();
            }
            else if (refreshTokenParameters.RevokedStatus == RevokedStatus.Active)
            {
                return refreshTokens.Where(u => u.IsUsed == false).ToList();
            }
            else
            {
                return refreshTokens;
            }
        }
    }
}