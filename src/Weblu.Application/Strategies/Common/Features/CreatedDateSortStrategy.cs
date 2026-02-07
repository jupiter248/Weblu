using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Domain.Entities.Common.Features;
using Weblu.Application.Parameters.Common;
using Weblu.Application.Interfaces.Strategies.Common;

namespace Weblu.Application.Strategies.Common.Features
{
    public class CreatedDateSortStrategy : IFeatureQueryStrategy
    {
        public IQueryable<Feature> Query(IQueryable<Feature> features, FeatureParameters featureParameters)
        {
            if (featureParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return features.OrderByDescending(s => s.CreatedAt);
            }
            else if (featureParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return features.OrderBy(s => s.CreatedAt);
            }
            else
            {
                return features;
            }
        }
    }
}