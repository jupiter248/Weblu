using Weblu.Domain.Entities.Common.Methods;
using Weblu.Application.Parameters.Common;
using Weblu.Application.Interfaces.Strategies.Common;

namespace Weblu.Application.Strategies.Common.Methods
{
    public class MethodQueryHandler
    {
        private IMethodQueryStrategy _methodQueryStrategy;
        public MethodQueryHandler(IMethodQueryStrategy methodQueryStrategy)
        {
            _methodQueryStrategy = methodQueryStrategy;
        }
        public IQueryable<Method> ExecuteMethodQuery(IQueryable<Method> methods, MethodParameters methodParameters)
        {
            return _methodQueryStrategy.Query(methods, methodParameters);
        }
    }
}