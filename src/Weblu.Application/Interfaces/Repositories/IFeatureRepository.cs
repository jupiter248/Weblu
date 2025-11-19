using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Services;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IFeatureRepository
    {
        Task<IReadOnlyList<Feature>> GetAllFeaturesAsync(FeatureParameters featureParameters);
        Task<Feature?> GetFeatureByIdAsync(int featureId);
        Task<bool> FeatureExistsAsync(int featureId);
        Task AddFeatureAsync(Feature feature);
        void UpdateFeature(Feature feature);
        void DeleteFeature(Feature feature);
    }
}