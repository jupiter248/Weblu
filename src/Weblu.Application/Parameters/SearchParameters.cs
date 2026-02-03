using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Common.Parameters;
using Weblu.Domain.Enums.Common.Search;

namespace Weblu.Application.Parameters
{
    public class SearchParameters : BaseParameters
    {
        public SearchEntityType SearchEntityType { get; set; }
    }
}