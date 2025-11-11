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
            IQueryable<RefreshToken> refreshTokens = _context.RefreshTokens.AsQueryable();

            var filterByUserId = new RefreshTokenQueryHandler(new FilterByUserIdStrategy());
            refreshTokens = filterByUserId.ExecuteServiceQuery(refreshTokens, refreshTokenParameters);

            var createdDateSort = new RefreshTokenQueryHandler(new CreatedDateSortStrategy());
            refreshTokens = createdDateSort.ExecuteServiceQuery(refreshTokens, refreshTokenParameters);

            var usedStatus = new RefreshTokenQueryHandler(new UsedStatusStrategy());
            refreshTokens = usedStatus.ExecuteServiceQuery(refreshTokens, refreshTokenParameters);

            var revokedStatus = new RefreshTokenQueryHandler(new RevokedStatusStrategy());
            refreshTokens = revokedStatus.ExecuteServiceQuery(refreshTokens, refreshTokenParameters);

            return await refreshTokens.ToListAsync();
        }

        public async Task<RefreshToken?> GetRefreshTokenByIdAsync(int refreshTokenId)
        {
            RefreshToken? refreshTokenModel = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Id == refreshTokenId);
            if (refreshTokenModel == null)
            {
                return null;
            }
            return refreshTokenModel;
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