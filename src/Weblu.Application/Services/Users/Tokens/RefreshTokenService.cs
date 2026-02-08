using AutoMapper;
using Weblu.Application.Dtos.Users.Tokens.TokenDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Repositories.Users.Tokens;
using Weblu.Application.Interfaces.Services.Users.Tokens;
using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users.Tokens;
using Weblu.Domain.Errors.Users;

namespace Weblu.Application.Services.Users.Tokens
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        private readonly IMapper _mapper;
        public RefreshTokenService(IUnitOfWork unitOfWork, IMapper mapper , 
            IRefreshTokenRepository refreshTokenRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<List<RefreshTokenDto>> GetAllAsync(RefreshTokenParameters refreshTokenParameters)
        {
            IReadOnlyList<RefreshToken> refreshTokens = await _refreshTokenRepository.GetAllAsync(refreshTokenParameters);
            List<RefreshTokenDto> refreshTokenDtos = _mapper.Map<List<RefreshTokenDto>>(refreshTokens);
            return refreshTokenDtos;
        }

        public async Task<RefreshTokenDto> GetByTokenAsync(string refreshToken)
        {
            RefreshToken refreshTokenModel = await _refreshTokenRepository.GetByTokenAsync(refreshToken) ?? throw new NotFoundException(TokenErrorCodes.RefreshTokenNotFound);
            RefreshTokenDto refreshTokenDto = _mapper.Map<RefreshTokenDto>(refreshTokenModel);
            return refreshTokenDto;
        }

        public async Task<RefreshTokenDto> UpdateAsync(int refreshTokenId, UpdateRefreshTokenDto updateRefreshTokenDto)
        {
            RefreshToken refreshToken = await _refreshTokenRepository.GetByIdAsync(refreshTokenId) ?? throw new NotFoundException(TokenErrorCodes.RefreshTokenNotFound);
            refreshToken = _mapper.Map(updateRefreshTokenDto, refreshToken);

            _refreshTokenRepository.Update(refreshToken);
            await _unitOfWork.CommitAsync();
            
            RefreshTokenDto refreshTokenDto = _mapper.Map<RefreshTokenDto>(refreshToken);
            return refreshTokenDto;
        }
    }
}