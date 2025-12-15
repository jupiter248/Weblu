using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Methods;
using Weblu.Application.Common.Interfaces;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IMethodRepository: IGenericRepository<Method, MethodParameters>
    {
    }
}