using Weblu.Application.Dtos.Common.MethodDtos;
using Weblu.Application.Parameters.Common;

namespace Weblu.Application.Interfaces.Services.Common
{
    public interface IMethodService
    {
        Task<List<MethodDto>> GetAllMethodsAsync(MethodParameters methodParameters);
        Task<MethodDto> GetMethodByIdAsync(int methodId);
        Task<MethodDto> AddMethodAsync(AddMethodDto addMethodDto);
        Task<MethodDto> UpdateMethodAsync(int methodId, UpdateMethodDto updateMethodDto);
        Task<MethodDto> UpdateMethodImageAsync(int methodId, UpdateMethodImageDto updateMethodImageDto);
        Task DeleteMethodImageAsync(int methodId);
        Task DeleteMethodAsync(int methodId);
    }
}