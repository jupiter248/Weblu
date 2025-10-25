using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Application.Strategies.RefreshTokens;
using Weblu.Domain.Entities.Users;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Token;

namespace Weblu.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;
        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
        }

        public async Task<List<RefreshToken>> GetAllRefreshTokenAsync(RefreshTokenParameters refreshTokenParameters)
        {
            List<RefreshToken> refreshTokens = await _context.RefreshTokens.ToListAsync();

            var filterByUserId = new RefreshTokenQueryHandler(new FilterByUserIdStrategy());
            refreshTokens = filterByUserId.ExecuteServiceQuery(refreshTokens, refreshTokenParameters);

            return refreshTokens;
        }

        public async Task<RefreshToken?> GetRefreshTokenByTokenAsync(string refreshToken)
        {
            RefreshToken? refreshTokenModel = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == refreshToken);
            if (refreshTokenModel == null)
            {
                return null;
            }
            return refreshTokenModel;
        }

        public void UpdateRefreshToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);
        }
    }
}