using AutoMapper;
using Weblu.Application.DTOs.Users.Tokens.TokenDTOs;
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
        public RefreshTokenService(IUnitOfWork unitOfWork, IMapper mapper,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<List<RefreshTokenDTO>> GetAllAsync(RefreshTokenParameters refreshTokenParameters)
        {
            IReadOnlyList<RefreshToken> refreshTokens = await _refreshTokenRepository.GetAllAsync(refreshTokenParameters);
            List<RefreshTokenDTO> refreshTokenDTOs = _mapper.Map<List<RefreshTokenDTO>>(refreshTokens);
            return refreshTokenDTOs;
        }

        public async Task<RefreshTokenDTO> GetByTokenAsync(string refreshToken)
        {
            RefreshToken refreshTokenModel = await _refreshTokenRepository.GetByTokenAsync(refreshToken) ?? throw new NotFoundException(TokenErrorCodes.RefreshTokenNotFound);
            RefreshTokenDTO refreshTokenDTO = _mapper.Map<RefreshTokenDTO>(refreshTokenModel);
            return refreshTokenDTO;
        }

        public async Task<RefreshTokenDTO> UpdateAsync(int refreshTokenId, UpdateRefreshTokenDTO updateRefreshTokenDTO)
        {
            RefreshToken refreshToken = await _refreshTokenRepository.GetByIdAsync(refreshTokenId) ?? throw new NotFoundException(TokenErrorCodes.RefreshTokenNotFound);
            refreshToken = _mapper.Map(updateRefreshTokenDTO, refreshToken);

            refreshToken.MarkUpdated();
            _refreshTokenRepository.Update(refreshToken);
            await _unitOfWork.CommitAsync();

            RefreshTokenDTO refreshTokenDTO = _mapper.Map<RefreshTokenDTO>(refreshToken);
            return refreshTokenDTO;
        }
    }
}