using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.TokenDtos
{
    public class UpdateRefreshTokenDto
    {
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
    }
}