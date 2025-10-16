using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Application.Parameters;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IMethodRepository
    {
        public Task<List<Method>> GetAllMethodsAsync(MethodParameters methodParameters);
        public Task<Method?> GetMethodByIdAsync(int methodId);
        public Task<bool> MethodExistsAsync(int methodId);
        public Task AddMethodAsync(Method method);
        public void UpdateMethod(Method method);
        public void DeleteMethod(Method method);
    }
}