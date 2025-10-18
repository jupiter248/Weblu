using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Users;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        public Task AddRefreshTokenAsync(RefreshToken refreshToken);
        public void UpdateRefreshToken(RefreshToken refreshToken);
        public Task<List<RefreshToken>> GetAllRefreshTokenAsync();
        public Task<RefreshToken?> GetRefreshTokenByTokenAsync(string refreshToken);


    }
}