using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IServiceRepository Services { get; }
        IFeatureRepository Features { get; }
        IMethodRepository Methods { get; }
        IImageRepository Images { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        IProfileImageRepository Profiles { get; }
        IUserRepository Users { get; }





        Task<int> CommitAsync();
    }
}