using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Users;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task AddRefreshTokenAsync(RefreshToken refreshToken);
        void UpdateRefreshToken(RefreshToken refreshToken);
        Task<List<RefreshToken>> GetAllRefreshTokenAsync();
        Task<RefreshToken?> GetRefreshTokenByTokenAsync(string refreshToken);


    }
}