using Weblu.Application.Dtos.Common.FeatureDtos;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Common;

namespace Weblu.Application.Interfaces.Services.Common
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