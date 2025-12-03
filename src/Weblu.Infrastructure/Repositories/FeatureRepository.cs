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

namespace Weblu.Infrastructure.Repositories
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly ApplicationDbContext _context;
        public FeatureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddFeatureAsync(Feature feature)
        {
            await _context.Features.AddAsync(feature);
        }

        public void DeleteFeature(Feature feature)
        {
            _context.Features.Remove(feature);
        }

        public async Task<bool> FeatureExistsAsync(int featureId)
        {
            bool featureExists = await _context.Features.AnyAsync(f => f.Id == featureId);
            return featureExists;
        }

        public async Task<IReadOnlyList<Feature>> GetAllFeaturesAsync(FeatureParameters featureParameters)
        {
            IQueryable<Feature> features = _context.Features.AsQueryable();
            if (featureParameters.CreatedDateSort != CreatedDateSort.All)
            {
                features = new FeatureQueryHandler(new CreatedDateSortStrategy())
                .ExecuteFeatureQuery(features, featureParameters);
            }

            return await features.ToListAsync();
        }

        public async Task<Feature?> GetFeatureByIdAsync(int featureId)
        {
            Feature? feature = await _context.Features.FirstOrDefaultAsync(f => f.Id == featureId);
            if (feature == null)
            {
                return null;
            }
            return feature;
        }

        public void UpdateFeature(Feature feature)
        {
            _context.Features.Update(feature);
        }
    }
}