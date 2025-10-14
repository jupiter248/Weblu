using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Parameters;

namespace Weblu.Application.Interfaces.Strategies
{
    public interface IMethodQueryStrategy
    {
        List<Method> Query(List<Method> methods, MethodParameters methodParameters);

    }
}