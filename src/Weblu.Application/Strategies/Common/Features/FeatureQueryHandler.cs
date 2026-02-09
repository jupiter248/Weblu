using Weblu.Domain.Entities.Common.Features;
using Weblu.Application.Interfaces.Strategies.Common;
using Weblu.Application.Parameters.Common;

namespace Weblu.Application.Strategies.Common.Features
{
    public class FeatureQueryHandler
    {
        private IFeatureQueryStrategy _featureQueryStrategy;
        public FeatureQueryHandler(IFeatureQueryStrategy featureQueryStrategy)
        {
            _featureQueryStrategy = featureQueryStrategy;
        }
        public IQueryable<Feature> ExecuteFeatureQuery(IQueryable<Feature> features, FeatureParameters featureParameters)
        {
            return _featureQueryStrategy.Query(features, featureParameters);
        } 
   }
}