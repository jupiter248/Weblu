using Weblu.Application.Common.Parameters;

namespace Weblu.Application.Parameters.Common
{
    public class ContributorParameters : BaseParameters
    {
        public int? FilterByPortfolioId { get; set; }
        public int? FilterByArticleId { get; set; }

    }
}