using Weblu.Application.Common.Parameters;
using Weblu.Domain.Enums.Services.Parameters;

namespace Weblu.Application.Parameters.Services
{
    public class ServiceParameters : BaseParameters
    {
        public PriceSort PriceSort { get; set; }
        public DurationSort DurationSort { get; set; }
    }
}