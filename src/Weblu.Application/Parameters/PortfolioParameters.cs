using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Parameters
{
    public class PortfolioParameters
    {
        public CreatedDateSort CreatedDateSort { get; set; }
        public int? CategoryId { get; set; }
        public int? ContributorId { get; set; }
    }
}