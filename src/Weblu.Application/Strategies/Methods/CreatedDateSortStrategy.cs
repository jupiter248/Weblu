using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Common.Methods;

namespace Weblu.Application.Strategies.Methods
{
    public class CreatedDateSortStrategy : IMethodQueryStrategy
    {
        public IQueryable<Method> Query(IQueryable<Method> methods, MethodParameters methodParameters)
        {
            if (methodParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return methods.OrderByDescending(s => s.CreatedAt);
            }
            else if (methodParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return methods.OrderBy(s => s.CreatedAt);
            }
            else
            {
                return methods;
            }
        }
    }
}