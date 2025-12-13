using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Parameters
{
    public class ContributorParameters
    {
        public CreatedDateSort CreatedDateSort { get; set; }
        public int? FilterByPortfolioId { get; set; }
        public int? FilterByArticleId { get; set; }

    }
}