using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Domain.Entities;
using Weblu.Domain.Parameters;

namespace Weblu.Application.Strategies.Features
{
    public class FeatureQueryHandler
    {
        private IFeatureQueryStrategy _featureQueryStrategy;
        public FeatureQueryHandler(IFeatureQueryStrategy featureQueryStrategy)
        {
            _featureQueryStrategy = featureQueryStrategy;
        }
        public List<Feature> ExecuteServiceQuery(List<Feature> features, FeatureParameters featureParameters)
        {
            return _featureQueryStrategy.Query(features, featureParameters);
        } 
   }
}