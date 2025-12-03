using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}