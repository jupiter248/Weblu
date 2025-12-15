using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Features;

namespace Weblu.Application.Interfaces.Strategies
{
    public interface IFeatureQueryStrategy
    {
        IQueryable<Feature> Query(IQueryable<Feature> features, FeatureParameters featureParameters);
    }
}