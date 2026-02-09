using Weblu.Application.DTOs.Common.ContributorDTOs;
using Weblu.Application.DTOs.Common.MethodDTOs;
using Weblu.Application.Parameters.Common;

namespace Weblu.Application.Interfaces.Services.Common
{
    public interface IMethodService
    {
        Task<List<MethodDTO>> GetAllAsync(MethodParameters methodParameters);
        Task<MethodDTO> GetByIdAsync(int methodId);
        Task<MethodDTO> CreateAsync(CreateMethodDTO createMethodDTO);
        Task<MethodDTO> UpdateAsync(int methodId, UpdateMethodDTO updateMethodDTO);
        Task<MethodDTO> ChangeImageAsync(int methodId, ChangeMethodImageDTO changeImageDTO);
        Task DeleteImageAsync(int methodId);
        Task DeleteAsync(int methodId);
    }
}