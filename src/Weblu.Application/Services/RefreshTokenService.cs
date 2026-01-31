using AutoMapper;
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
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        private readonly IMapper _mapper;
        public RefreshTokenService(IUnitOfWork unitOfWork, IMapper mapper , 
            IRefreshTokenRepository refreshTokenRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<List<RefreshTokenDto>> GetAllRefreshTokensAsync(RefreshTokenParameters refreshTokenParameters)
        {
            IReadOnlyList<RefreshToken> refreshTokens = await _refreshTokenRepository.GetAllAsync(refreshTokenParameters);
            List<RefreshTokenDto> refreshTokenDtos = _mapper.Map<List<RefreshTokenDto>>(refreshTokens);
            return refreshTokenDtos;
        }

        public async Task<RefreshTokenDto> GetRefreshTokenByToken(string refreshToken)
        {
            RefreshToken refreshTokenModel = await _refreshTokenRepository.GetByTokenAsync(refreshToken) ?? throw new NotFoundException(TokenErrorCodes.RefreshTokenNotFound);
            RefreshTokenDto refreshTokenDto = _mapper.Map<RefreshTokenDto>(refreshTokenModel);
            return refreshTokenDto;
        }

        public async Task<RefreshTokenDto> UpdateRefreshToken(int refreshTokenId, UpdateRefreshTokenDto updateRefreshTokenDto)
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