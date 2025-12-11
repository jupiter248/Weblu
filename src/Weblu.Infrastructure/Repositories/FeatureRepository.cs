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

namespace Weblu.Infrastructure.Repositories
{
    internal class FeatureRepository : GenericRepository<Feature, FeatureParameters>, IFeatureRepository
    {
        public FeatureRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<IReadOnlyList<Feature>> GetAllAsync(FeatureParameters featureParameters)
        {
            IQueryable<Feature> features = _context.Features.AsQueryable();
            if (featureParameters.CreatedDateSort != CreatedDateSort.All)
            {
                features = new FeatureQueryHandler(new CreatedDateSortStrategy())
                .ExecuteFeatureQuery(features, featureParameters);
            }

            return await features.ToListAsync();
        }

        public override async Task<Feature?> GetByIdAsync(int featureId)
        {
            Feature? feature = await _context.Features.FirstOrDefaultAsync(f => f.Id == featureId);
            if (feature == null)
            {
                return null;
            }
            return feature;
        }
    }
}