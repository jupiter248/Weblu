using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Users;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task AddRefreshTokenAsync(RefreshToken refreshToken);
        void UpdateRefreshToken(RefreshToken refreshToken);
        Task<IReadOnlyList<RefreshToken>> GetAllRefreshTokenAsync(RefreshTokenParameters refreshTokenParameters);
        Task<RefreshToken?> GetRefreshTokenByTokenAsync(string refreshToken);
        Task<RefreshToken?> GetRefreshTokenByIdAsync(int refreshTokenId);



    }
}