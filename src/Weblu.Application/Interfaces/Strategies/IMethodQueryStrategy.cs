using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;

namespace Weblu.Application.Interfaces.Strategies
{
    public interface IMethodQueryStrategy
    {
        IQueryable<Method> Query(IQueryable<Method> methods, MethodParameters methodParameters);

    }
}