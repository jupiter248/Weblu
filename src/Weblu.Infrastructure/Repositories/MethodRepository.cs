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
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Methods;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Common.Pagination;
using Weblu.Infrastructure.Common.Pagination;

namespace Weblu.Infrastructure.Repositories
{
    internal class MethodRepository : GenericRepository<Method, MethodParameters>, IMethodRepository
    {
        public MethodRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<PagedList<Method>> GetAllAsync(MethodParameters methodParameters)
        {
            IQueryable<Method> methods = _context.Methods.Where(a => !a.IsDeleted).AsNoTracking();

            if (methodParameters.CreatedDateSort != CreatedDateSort.All)
            {
                methods = new MethodQueryHandler(new CreatedDateSortStrategy())
                .ExecuteMethodQuery(methods, methodParameters);
            }
            if (methodParameters.FilterByServiceId.HasValue)
            {
                methods = new MethodQueryHandler(new FilterByServiceIdStrategy())
                .ExecuteMethodQuery(methods.Include(s => s.Services), methodParameters);
            }
            if (methodParameters.FilterByPortfolioId.HasValue)
            {
                methods = new MethodQueryHandler(new FilterByPortfolioIdStrategy())
                .ExecuteMethodQuery(methods.Include(p => p.Portfolios), methodParameters);
            }
            return await PaginationExtensions<Method>.GetPagedList(methods, methodParameters.PageNumber, methodParameters.PageSize);

        }
    }
}