using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Dtos.FeatureDtos;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Strategies.Features;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Application.Parameters;
using Weblu.Infrastructure.Data;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Application.Common.Interfaces;
using Weblu.Domain.Entities.Features;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Common.Pagination;
using Weblu.Infrastructure.Common.Pagination;


namespace Weblu.Infrastructure.Repositories
{
    internal class FeatureRepository : GenericRepository<Feature, FeatureParameters>, IFeatureRepository
    {
        public FeatureRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<PagedList<Feature>> GetAllAsync(FeatureParameters featureParameters)
        {
            IQueryable<Feature> features = _context.Features.AsNoTracking().AsQueryable();
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