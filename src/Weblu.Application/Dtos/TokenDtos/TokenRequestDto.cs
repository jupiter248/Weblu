using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.RefreshTokenDtos
{
    public class TokenRequestDto
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }

    }
}