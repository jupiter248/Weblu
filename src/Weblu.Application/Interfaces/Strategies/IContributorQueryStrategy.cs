using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;

namespace Weblu.Application.Interfaces.Strategies
{
    public interface IContributorQueryStrategy
    {
        List<Contributor> Query(List<Contributor> contributors, ContributorParameters contributorParameters);
    }
}