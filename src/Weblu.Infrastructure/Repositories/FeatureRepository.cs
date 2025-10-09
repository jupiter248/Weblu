using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Dtos.FeatureDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Strategies.Features;
using Weblu.Domain.Entities;
using Weblu.Domain.Interfaces;
using Weblu.Domain.Parameters;
using Weblu.Infrastructure.Data;

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

        public async Task<List<Feature>> GetAllFeaturesAsync(FeatureParameters featureParameters)
        {
            List<Feature> features = await _context.Features.ToListAsync();

            var createdDateSortQuery = new FeatureQueryHandler(new CreatedDateSortStrategy());
            features = createdDateSortQuery.ExecuteServiceQuery(features, featureParameters);
            
            return features;
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