using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Domain.Entities.Common.Methods;
using Weblu.Application.Parameters.Common;
using Weblu.Application.Interfaces.Strategies.Common;

namespace Weblu.Application.Strategies.Common.Methods
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