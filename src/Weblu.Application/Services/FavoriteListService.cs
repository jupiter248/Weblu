using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.FavoriteListDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Favorites;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Errors.Favorites;
using Weblu.Domain.Errors.Portfolios;
using Weblu.Domain.Errors.Users;

namespace Weblu.Application.Services
{
    public class FavoriteListService : IFavoriteListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFavoriteListRepository _favoriteListRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IUserFavoritesRepository _userFavoritesRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public FavoriteListService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserRepository userRepository,
            IFavoriteListRepository favoriteListRepository,
            IPortfolioRepository portfolioRepository,
            IUserFavoritesRepository userFavoritesRepository
            )
        {
            _favoriteListRepository = favoriteListRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userFavoritesRepository = userFavoritesRepository;
            _portfolioRepository = portfolioRepository;
        }

        public async Task<FavoriteListDto> AddFavoriteListAsync(string userId, AddFavoriteListDto addFavoriteListDto)
        {
            bool userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            FavoriteList favoriteList = _mapper.Map<FavoriteList>(addFavoriteListDto);
            favoriteList.UserId = userId;

            await _favoriteListRepository.AddFavoriteListAsync(favoriteList);
            await _unitOfWork.CommitAsync();

            FavoriteListDto favoriteListDto = _mapper.Map<FavoriteListDto>(favoriteList);
            return favoriteListDto;
        }

        public async Task AddPortfolioToFavoriteListAsync(string userId, int favoriteListId, int portfolioId)
        {
            bool userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            FavoriteList favoriteList = await _favoriteListRepository.GetFavoriteListByIdAsync(favoriteListId) ?? throw new NotFoundException(FavoriteListErrorCodes.NotFound);
            Portfolio portfolio = await _portfolioRepository.GetPortfolioByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            FavoritePortfolio favoritePortfolio = await _userFavoritesRepository.GetFavoritePortfolioByPortfolioIdAsync(userId, portfolio.Id) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);

            if (favoriteList.FavoritePortfolios.Any(p => p.Id == favoritePortfolio.Id))
            {
                throw new ConflictException(FavoriteListErrorCodes.PortfolioAlreadyAddedToFavoriteList);
            }

            favoriteList.FavoritePortfolios.Add(favoritePortfolio);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteFavoriteListAsync(string userId, int favoriteListId)
        {
            bool userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            FavoriteList favoriteList = await _favoriteListRepository.GetFavoriteListByIdAsync(favoriteListId) ?? throw new NotFoundException(FavoriteListErrorCodes.NotFound);
            if (favoriteList.UserId != userId)
            {
                throw new UnauthorizedException(FavoriteListErrorCodes.DeleteForbidden);
            }
            _favoriteListRepository.DeleteFavoriteList(favoriteList);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeletePortfolioFromFavoriteListAsync(string userId, int favoriteListId, int portfolioId)
        {
            bool userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            FavoriteList favoriteList = await _favoriteListRepository.GetFavoriteListByIdAsync(favoriteListId) ?? throw new NotFoundException(FavoriteListErrorCodes.NotFound);
            Portfolio portfolio = await _portfolioRepository.GetPortfolioByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            FavoritePortfolio favoritePortfolio = await _userFavoritesRepository.GetFavoritePortfolioByPortfolioIdAsync(userId, portfolio.Id) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);

            if (!favoriteList.FavoritePortfolios.Any(p => p.Id == favoritePortfolio.Id))
            {
                throw new ConflictException(PortfolioErrorCodes.PortfolioNotFound);
            }

            favoriteList.FavoritePortfolios.Remove(favoritePortfolio);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<FavoriteListDto>> GetAllFavoriteListsAsync(string userId, FavoriteListParameters favoriteListParameters)
        {
            IReadOnlyList<FavoriteList> favoriteLists = await _favoriteListRepository.GetAllFavoriteListsAsync(userId, favoriteListParameters);
            List<FavoriteListDto> favoriteListDtos = _mapper.Map<List<FavoriteListDto>>(favoriteLists);
            return favoriteListDtos;
        }

        public async Task<FavoriteListDto> GetFavoriteListByIdAsync(int favoriteListId)
        {
            FavoriteList favoriteList = await _favoriteListRepository.GetFavoriteListByIdAsync(favoriteListId) ?? throw new NotFoundException(FavoriteListErrorCodes.NotFound);
            FavoriteListDto favoriteListDto = _mapper.Map<FavoriteListDto>(favoriteList);
            return favoriteListDto;
        }

        public async Task<FavoriteListDto> UpdateFavoriteListAsync(string userId, int favoriteListId, UpdateFavoriteListDto updateFavoriteListDto)
        {
            bool userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            FavoriteList favoriteList = await _favoriteListRepository.GetFavoriteListByIdAsync(favoriteListId) ?? throw new NotFoundException(FavoriteListErrorCodes.NotFound);
            if (favoriteList.UserId != userId)
            {
                throw new UnauthorizedException(FavoriteListErrorCodes.UpdateForbidden);
            }
            favoriteList = _mapper.Map(updateFavoriteListDto, favoriteList);

            _favoriteListRepository.UpdateFavoriteList(favoriteList);
            await _unitOfWork.CommitAsync();

            FavoriteListDto favoriteListDto = _mapper.Map<FavoriteListDto>(favoriteList);
            return favoriteListDto;
        }
    }
}