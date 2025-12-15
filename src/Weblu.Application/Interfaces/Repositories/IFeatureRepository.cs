using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Features;
using Weblu.Domain.Entities.Services;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IFeatureRepository : IGenericRepository<Feature, FeatureParameters>
    {
    }
}