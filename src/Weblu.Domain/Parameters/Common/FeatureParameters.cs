using Weblu.Application.Common.Parameters;

namespace Weblu.Application.Parameters.Common
{
    public class FeatureParameters : BaseParameters
    {
        public int? FilterByServiceId { get; set; }
        public int? FilterByPortfolioId { get; set; }
    }
}