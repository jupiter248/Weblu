using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IMethodRepository
    {
        Task<List<Method>> GetAllMethodsAsync(MethodParameters methodParameters);
        Task<Method?> GetMethodByIdAsync(int methodId);
        Task<bool> MethodExistsAsync(int methodId);
        Task AddMethodAsync(Method method);
        void UpdateMethod(Method method);
        void DeleteMethod(Method method);
    }
}