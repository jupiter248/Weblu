using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users.Tokens;

namespace Weblu.Domain.Interfaces.Repositories.Users.Tokens
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken, RefreshTokenParameters>
    {
        Task<RefreshToken?> GetByTokenAsync(string refreshToken);
    }
}