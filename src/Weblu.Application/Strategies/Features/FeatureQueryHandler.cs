using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;

namespace Weblu.Application.Strategies.Features
{
    public class FeatureQueryHandler
    {
        private IFeatureQueryStrategy _featureQueryStrategy;
        public FeatureQueryHandler(IFeatureQueryStrategy featureQueryStrategy)
        {
            _featureQueryStrategy = featureQueryStrategy;
        }
        public IQueryable<Feature> ExecuteServiceQuery(IQueryable<Feature> features, FeatureParameters featureParameters)
        {
            return _featureQueryStrategy.Query(features, featureParameters);
        } 
   }
}