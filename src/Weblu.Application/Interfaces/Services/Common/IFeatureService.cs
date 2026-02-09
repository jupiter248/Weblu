using Weblu.Application.DTOs.Common.FeatureDTOs;
using Weblu.Application.Parameters.Common;

namespace Weblu.Application.Interfaces.Services.Common
{
    public interface IFeatureService
    {
        Task<List<FeatureDTO>> GetAllAsync(FeatureParameters featureParameters);
        Task<FeatureDTO> GetByIdAsync(int featureId);
        Task<FeatureDTO> CreateAsync(CreateFeatureDTO createFeatureDTO);
        Task<FeatureDTO> UpdateAsync(int featureId, UpdateFeatureDTO updateFeatureDTO);
        Task DeleteAsync(int featureId);
    }
}