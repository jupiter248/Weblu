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

namespace Weblu.Application.Strategies.Methods
{
    public class CreatedDateSortStrategy : IMethodQueryStrategy
    {
        public List<Method> Query(List<Method> methods, MethodParameters methodParameters)
        {
            if (methodParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return methods.OrderByDescending(s => s.CreatedAt).ToList();
            }
            else if (methodParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return methods.OrderBy(s => s.CreatedAt).ToList();
            }
            else
            {
                return methods;
            }
        }
    }
}