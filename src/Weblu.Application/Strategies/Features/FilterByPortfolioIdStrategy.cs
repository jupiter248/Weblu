using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;

namespace Weblu.Application.Strategies.Features
{
    public class FilterByPortfolioIdStrategy : IFeatureQueryStrategy
    {
        public IQueryable<Feature> Query(IQueryable<Feature> features, FeatureParameters featureParameters)
        {
            return features.Where(f => f.Portfolios.Any(p => p.Id == featureParameters.FilterByPortfolioId));
        }
    }
}