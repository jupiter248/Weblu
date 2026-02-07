using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Methods;
using Weblu.Application.Common.Interfaces;

namespace Weblu.Application.Interfaces.Repositories.Common
{
    public interface IMethodRepository: IGenericRepository<Method, MethodParameters>
    {
    }
}