using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Features;

namespace Weblu.Application.Interfaces.Strategies.Common
{
    public interface IFeatureQueryStrategy
    {
        IQueryable<Feature> Query(IQueryable<Feature> features, FeatureParameters featureParameters);
    }
}