using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Methods;

namespace Weblu.Application.Strategies.Methods
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