using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Weblu.Application.Dtos.RefreshTokenDtos;
using Weblu.Application.Dtos.TokenDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Domain.Entities.Users;

namespace Weblu.Application.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RefreshTokenService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<RefreshTokenDto>> GetAllRefreshTokensAsync()
        {
            List<RefreshToken> refreshTokens = await _unitOfWork.RefreshTokens.GetAllRefreshTokenAsync();
            List<RefreshTokenDto> refreshTokenDtos = _mapper.Map<List<RefreshTokenDto>>(refreshTokens);
            return refreshTokenDtos;
        }
    }
}