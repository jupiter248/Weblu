using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IServiceRepository Services { get; }
        IFeatureRepository Features { get; }
        IMethodRepository Methods { get; }
        IImageRepository Images { get; }


        Task<int> CommitAsync();
    }
}