using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Common.Parameters;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Domain.Enums.Services.Parameters;

namespace Weblu.Application.Parameters
{
    public class ServiceParameters : BaseParameters
    {
        public PriceSort PriceSort { get; set; }
        public DurationSort DurationSort { get; set; }
    }
}