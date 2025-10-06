using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities;
using Weblu.Domain.Parameters;

namespace Weblu.Domain.Interfaces
{
    public interface IFeatureRepository
    {
        Task<List<Feature>> GetAllFeaturesAsync(FeatureParameters featureParameters);
        Task<Feature?> GetFeatureByIdAsync(int featureId);
        Task<bool> FeatureExistsAsync(int featureId);
        Task AddFeatureAsync(Feature feature);
        void UpdateFeature(Feature feature);
        void DeleteFeature(Feature feature);
    }
}