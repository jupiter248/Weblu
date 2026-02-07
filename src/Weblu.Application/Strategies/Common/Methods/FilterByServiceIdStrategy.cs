using Weblu.Application.Interfaces.Strategies.Common;
using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Methods;

namespace Weblu.Application.Strategies.Common.Methods
{
    public class FilterByServiceIdStrategy : IMethodQueryStrategy
    {
        public IQueryable<Method> Query(IQueryable<Method> methods, MethodParameters methodParameters)
        {
            return methods.Where(f => f.Services.Any(s => s.Id == methodParameters.FilterByServiceId));
        }
    }
}