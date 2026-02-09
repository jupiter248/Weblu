using Weblu.Application.Interfaces.Strategies.Common;
using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Features;

namespace Weblu.Application.Strategies.Common.Features
{
    public class FilterByPortfolioIdStrategy : IFeatureQueryStrategy
    {
        public IQueryable<Feature> Query(IQueryable<Feature> features, FeatureParameters featureParameters)
        {
            return features.Where(f => f.Portfolios.Any(p => p.Id == featureParameters.FilterByPortfolioId));
        }
    }
}