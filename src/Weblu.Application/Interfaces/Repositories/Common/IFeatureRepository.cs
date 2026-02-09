using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Features;

namespace Weblu.Application.Interfaces.Repositories.Common
{
    public interface IFeatureRepository : IGenericRepository<Feature, FeatureParameters>
    {
    }
}