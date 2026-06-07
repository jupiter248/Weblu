using Weblu.Application.Common.Parameters;

namespace Weblu.Application.Parameters.Common
{
    public class MethodParameters : BaseParameters
    {
        public int? FilterByServiceId { get; set; }
        public int? FilterByPortfolioId { get; set; }
    }
}