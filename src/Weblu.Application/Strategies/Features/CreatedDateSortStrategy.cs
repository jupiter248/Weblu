using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Domain.Entities;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Domain.Parameters;

namespace Weblu.Application.Strategies.Features
{
    public class CreatedDateSortStrategy : IFeatureQueryStrategy
    {
        public List<Feature> Query(List<Feature> features, FeatureParameters featureParameters)
        {
            if (featureParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return features.OrderByDescending(s => s.CreatedAt).ToList();
            }
            else if (featureParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return features.OrderBy(s => s.CreatedAt).ToList();
            }
            else
            {
                return features;
            }
        }
    }
}