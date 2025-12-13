using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common.Methods;

namespace Weblu.Application.Strategies.Methods
{
    public class FilterByPortfolioIdStrategy : IMethodQueryStrategy
    {
        public IQueryable<Method> Query(IQueryable<Method> methods, MethodParameters methodParameters)
        {
            return methods.Where(f => f.Portfolios.Any(p => p.Id == methodParameters.FilterByPortfolioId));
        }
    }
}