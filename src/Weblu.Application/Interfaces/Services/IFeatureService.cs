using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.FeatureDtos;
using Weblu.Application.Parameters;

namespace Weblu.Application.Interfaces.Services
{
    public interface IFeatureService
    {
        Task<List<FeatureDto>> GetAllFeaturesAsync(FeatureParameters featureParameters);
        Task<FeatureDto> GetFeatureByIdAsync(int featureId);
        Task<FeatureDto> AddFeatureAsync(AddFeatureDto addFeatureDto);
        Task<FeatureDto> UpdateFeatureAsync(int featureId, UpdateFeatureDto updateFeatureDto);
        Task DeleteFeatureAsync(int featureId);
    }
}