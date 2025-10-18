using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Users;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        public void UpdateRefreshToken(RefreshToken refreshToken);
    }
}