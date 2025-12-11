using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IContributorRepository : IGenericRepository<Contributor, ContributorParameters>
    {
    }
}