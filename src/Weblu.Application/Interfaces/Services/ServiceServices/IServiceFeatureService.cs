using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Interfaces.Services.ServiceServices
{
    public interface IServiceFeatureService
    {
        Task AddFeatureAsync(int serviceId, int featureId);
        Task DeleteFeatureAsync(int serviceId, int featureId);
    }
}