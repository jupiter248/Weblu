using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Tags;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface ITagRepository : IGenericRepository<Tag , TagParameters>
    {
    }
}