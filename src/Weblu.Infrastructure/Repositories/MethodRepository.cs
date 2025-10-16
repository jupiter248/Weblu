using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Strategies.Methods;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Application.Parameters;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class MethodRepository : IMethodRepository
    {
        private readonly ApplicationDbContext _context;
        public MethodRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddMethodAsync(Method method)
        {
            await _context.Methods.AddAsync(method);
        }

        public void DeleteMethod(Method method)
        {
            _context.Methods.Remove(method);
        }

        public async Task<List<Method>> GetAllMethodsAsync(MethodParameters methodParameters)
        {
            List<Method> methods = await _context.Methods.ToListAsync();

            var createdDateSortQuery = new MethodQueryHandler(new CreatedDateSortStrategy());
            methods = createdDateSortQuery.ExecuteServiceQuery(methods, methodParameters);

            return methods;

        }

        public async Task<Method?> GetMethodByIdAsync(int methodId)
        {
            Method? method = await _context.Methods.FirstOrDefaultAsync(m => m.Id == methodId);
            if (method == null)
            {
                return null;
            }
            return method;
        }

        public async Task<bool> MethodExistsAsync(int methodId)
        {
            bool methodExists = await _context.Methods.AnyAsync(m => m.Id == methodId);
            return methodExists;
        }

        public void UpdateMethod(Method method)
        {
            _context.Methods.Update(method);
        }
    }
}