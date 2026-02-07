using Microsoft.EntityFrameworkCore;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Common.Pagination;
using Weblu.Infrastructure.Common.Pagination;
using Weblu.Application.Parameters.Users;
using Weblu.Application.Strategies.Tokens;
using Weblu.Domain.Enums.Users.Tokens;
using Weblu.Domain.Entities.Users.Tokens;
using Weblu.Application.Interfaces.Repositories.Users.Tokens;

namespace Weblu.Infrastructure.Repositories.Users.Tokens
{
    internal class RefreshTokenRepository : GenericRepository<RefreshToken, RefreshTokenParameters>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<PagedList<RefreshToken>> GetAllAsync(RefreshTokenParameters refreshTokenParameters)
        {
            IQueryable<RefreshToken> refreshTokens = _context.RefreshTokens.AsNoTracking();
            if (!string.IsNullOrEmpty(refreshTokenParameters.FilterByUserId))
            {
                refreshTokens = new RefreshTokenQueryHandler(new FilterByUserIdStrategy())
                .ExecuteRefreshTokenQuery(refreshTokens, refreshTokenParameters);
            }
            if (refreshTokenParameters.CreatedDateSort != CreatedDateSort.All)
            {
                refreshTokens = new RefreshTokenQueryHandler(new CreatedDateSortStrategy())
                .ExecuteRefreshTokenQuery(refreshTokens, refreshTokenParameters);
            }
            if (refreshTokenParameters.UsedStatus != UsedStatus.All)
            {
                refreshTokens = new RefreshTokenQueryHandler(new UsedStatusStrategy())
                .ExecuteRefreshTokenQuery(refreshTokens, refreshTokenParameters);
            }
            if (refreshTokenParameters.RevokedStatus != RevokedStatus.All)
            {
                refreshTokens = new RefreshTokenQueryHandler(new RevokedStatusStrategy())
                .ExecuteRefreshTokenQuery(refreshTokens, refreshTokenParameters);
            }

            return await PaginationExtensions<RefreshToken>.GetPagedList(refreshTokens, refreshTokenParameters.PageNumber, refreshTokenParameters.PageSize);

        }

        public async Task<RefreshToken?> GetByTokenAsync(string refreshToken)
        {
            RefreshToken? refreshTokenModel = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == refreshToken);
            if (refreshTokenModel == null)
            {
                return null;
            }
            return refreshTokenModel;
        }
    }
}