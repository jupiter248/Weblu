using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.RefreshTokenDtos;
using Weblu.Application.Dtos.TokenDtos;
using Weblu.Domain.Entities.Users;

namespace Weblu.Application.Interfaces.Services
{
    public interface IRefreshTokenService
    {
        public Task<List<RefreshTokenDto>> GetAllRefreshTokensAsync();
    }
}