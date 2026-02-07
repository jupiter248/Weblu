using Microsoft.EntityFrameworkCore;
using Weblu.Infrastructure.Data;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Common.Pagination;
using Weblu.Infrastructure.Common.Pagination;
using Weblu.Domain.Entities.Common.Features;
using Weblu.Application.Parameters.Common;
using Weblu.Application.Strategies.Common.Features;
using Weblu.Application.Interfaces.Repositories.Common;


namespace Weblu.Infrastructure.Repositories.Common
{
    internal class FeatureRepository : GenericRepository<Feature, FeatureParameters>, IFeatureRepository
    {
        public FeatureRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<PagedList<Feature>> GetAllAsync(FeatureParameters featureParameters)
        {
            IQueryable<Feature> features = _context.Features.Where(a => !a.IsDeleted).AsNoTracking().AsQueryable();
            if (featureParameters.CreatedDateSort != CreatedDateSort.All)
            {
                features = new FeatureQueryHandler(new CreatedDateSortStrategy())
                .ExecuteFeatureQuery(features, featureParameters);
            }
            if (featureParameters.FilterByServiceId.HasValue)
            {
                features = new FeatureQueryHandler(new FilterByServiceIdStrategy())
                .ExecuteFeatureQuery(features.Include(s => s.Services), featureParameters);
            }
            if (featureParameters.FilterByPortfolioId.HasValue)
            {
                features = new FeatureQueryHandler(new FilterByPortfolioIdStrategy())
                .ExecuteFeatureQuery(features.Include(p => p.Portfolios), featureParameters);
            }

            return await PaginationExtensions<Feature>.GetPagedList(features, featureParameters.PageNumber, featureParameters.PageSize);
        }
    }
}