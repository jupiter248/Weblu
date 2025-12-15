using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Contributors;

namespace Weblu.Application.Interfaces.Strategies
{
    public interface IContributorQueryStrategy
    {
        IQueryable<Contributor> Query(IQueryable<Contributor> contributors, ContributorParameters contributorParameters);
    }
}