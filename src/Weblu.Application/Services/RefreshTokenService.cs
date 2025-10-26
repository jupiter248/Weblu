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
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Users;
using Weblu.Domain.Errors.Tokens;

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
        public async Task<List<RefreshTokenDto>> GetAllRefreshTokensAsync(RefreshTokenParameters refreshTokenParameters)
        {
            List<RefreshToken> refreshTokens = await _unitOfWork.RefreshTokens.GetAllRefreshTokenAsync(refreshTokenParameters);
            List<RefreshTokenDto> refreshTokenDtos = _mapper.Map<List<RefreshTokenDto>>(refreshTokens);
            return refreshTokenDtos;
        }

        public async Task<RefreshTokenDto> GetRefreshTokenByToken(string refreshToken)
        {
            RefreshToken refreshTokenModel = await _unitOfWork.RefreshTokens.GetRefreshTokenByTokenAsync(refreshToken) ?? throw new NotFoundException(TokenErrorCodes.RefreshTokenNotFound);
            RefreshTokenDto refreshTokenDto = _mapper.Map<RefreshTokenDto>(refreshTokenModel);
            return refreshTokenDto;
        }

        public async Task<RefreshTokenDto> UpdateRefreshToken(int refreshTokenId, UpdateRefreshTokenDto updateRefreshTokenDto)
        {
            RefreshToken refreshToken = await _unitOfWork.RefreshTokens.GetRefreshTokenByIdAsync(refreshTokenId) ?? throw new NotFoundException(TokenErrorCodes.RefreshTokenNotFound);
            refreshToken = _mapper.Map(updateRefreshTokenDto, refreshToken);

            _unitOfWork.RefreshTokens.UpdateRefreshToken(refreshToken);
            await _unitOfWork.CommitAsync();
            
            RefreshTokenDto refreshTokenDto = _mapper.Map<RefreshTokenDto>(refreshToken);
            return refreshTokenDto;
        }
    }
}