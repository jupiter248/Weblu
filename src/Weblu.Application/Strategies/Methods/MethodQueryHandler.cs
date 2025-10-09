using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Domain.Entities;
using Weblu.Domain.Parameters;

namespace Weblu.Application.Strategies.Methods
{
    public class MethodQueryHandler
    {
        private IMethodQueryStrategy _methodQueryStrategy;
        public MethodQueryHandler(IMethodQueryStrategy methodQueryStrategy)
        {
            _methodQueryStrategy = methodQueryStrategy;
        }
        public List<Method> ExecuteServiceQuery(List<Method> methods, MethodParameters methodParameters)
        {
            return _methodQueryStrategy.Query(methods, methodParameters);
        }
    }
}