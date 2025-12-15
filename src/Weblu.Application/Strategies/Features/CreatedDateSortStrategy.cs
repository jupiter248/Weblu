using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Features;

namespace Weblu.Application.Strategies.Features
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