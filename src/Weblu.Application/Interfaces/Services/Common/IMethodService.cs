using Weblu.Application.Dtos.Common.ContributorDtos;
using Weblu.Application.Dtos.Common.MethodDtos;
using Weblu.Application.Parameters.Common;

namespace Weblu.Application.Interfaces.Services.Common
{
    public interface IMethodService
    {
        Task<List<MethodDto>> GetAllAsync(MethodParameters methodParameters);
        Task<MethodDto> GetByIdAsync(int methodId);
        Task<MethodDto> CreateAsync(CreateMethodDto createMethodDto);
        Task<MethodDto> UpdateAsync(int methodId, UpdateMethodDto updateMethodDto);
        Task<MethodDto> ChangeImageAsync(int methodId, ChangeMethodImageDto changeImageDto);
        Task DeleteImageAsync(int methodId);
        Task DeleteAsync(int methodId);
    }
}