using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Methods;

namespace Weblu.Application.Interfaces.Strategies.Common
{
    public interface IMethodQueryStrategy
    {
        IQueryable<Method> Query(IQueryable<Method> methods, MethodParameters methodParameters);

    }
}