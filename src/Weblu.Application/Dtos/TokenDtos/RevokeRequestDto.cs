using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.TokenDtos
{
    public class RevokeRequestDto
    {
        public required string RefreshToken { get; set; }
    }
}