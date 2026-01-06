using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Common.Parameters;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Domain.Enums.Tokens;

namespace Weblu.Application.Parameters
{
    public class RefreshTokenParameters : BaseParameters
    {
        public string? FilterByUserId { get; set; }
        public RevokedStatus RevokedStatus { get; set; }
        public UsedStatus UsedStatus { get; set; }

    }
}