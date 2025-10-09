using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.MethodDtos;
using Weblu.Domain.Parameters;

namespace Weblu.Application.Interfaces.Services
{
    public interface IMethodService
    {
        Task<List<MethodDto>> GetAllMethodsAsync(MethodParameters methodParameters);
        Task<MethodDto> GetMethodByIdAsync(int methodId);
        Task<MethodDto> AddMethodAsync(AddMethodDto addMethodDto);
        Task<MethodDto> UpdateMethodAsync(int methodId, UpdateMethodDto updateMethodDto);
        Task DeleteMethodAsync(int methodId);
    }
}