using Weblu.Application.Dtos.Common.FeatureDtos;
using Weblu.Application.Parameters.Common;

namespace Weblu.Application.Interfaces.Services.Common
{
    public interface IFeatureService
    {
        Task<List<FeatureDto>> GetAllAsync(FeatureParameters featureParameters);
        Task<FeatureDto> GetByIdAsync(int featureId);
        Task<FeatureDto> CreateAsync(CreateFeatureDto createFeatureDto);
        Task<FeatureDto> UpdateAsync(int featureId, UpdateFeatureDto updateFeatureDto);
        Task DeleteAsync(int featureId);
    }
}