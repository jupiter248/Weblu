using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Users;
using Weblu.Application.Interfaces.Repositories.Users.UserFavorites;
using Weblu.Application.Interfaces.Services.Users.UserFavorites.FavoriteLists;
using Weblu.Domain.Entities.Favorites;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Errors.Favorites;
using Weblu.Domain.Errors.Portfolios;
using Weblu.Domain.Errors.Users;

namespace Weblu.Application.Services.UserFavorites.FavoriteLists
{
    public class FavoriteListPortfolioService : IFavoriteListPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IUserPortfolioFavoriteRepository _userPortfolioFavoriteRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFavoriteListRepository _favoriteListRepository;

        public FavoriteListPortfolioService(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IFavoriteListRepository favoriteListRepository,
            IPortfolioRepository portfolioRepository,
            IUserPortfolioFavoriteRepository userPortfolioFavoriteRepository
            )
        {
            _favoriteListRepository = favoriteListRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _userPortfolioFavoriteRepository = userPortfolioFavoriteRepository;
            _portfolioRepository = portfolioRepository;
        }
        public async Task AddAsync(string userId, int favoriteListId, int portfolioId)
        {
            bool userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            FavoriteList favoriteList = await _favoriteListRepository.GetByUserAndListIdAsync(userId, favoriteListId) ?? throw new NotFoundException(FavoriteListErrorCodes.NotFound);
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            FavoritePortfolio favoritePortfolio = await _userPortfolioFavoriteRepository.GetByPortfolioIdAsync(userId, portfolio.Id) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);

            favoriteList.AddFavoritePortfolio(favoritePortfolio);
            await _unitOfWork.CommitAsync();
        }
        public async Task DeleteAsync(string userId, int favoriteListId, int portfolioId)
        {
            bool userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            FavoriteList favoriteList = await _favoriteListRepository.GetByUserAndListIdAsync(userId, favoriteListId) ?? throw new NotFoundException(FavoriteListErrorCodes.NotFound);
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            FavoritePortfolio favoritePortfolio = await _userPortfolioFavoriteRepository.GetByPortfolioIdAsync(userId, portfolio.Id) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);

            favoriteList.DeleteFavoritePortfolio(favoritePortfolio);
            await _unitOfWork.CommitAsync();
        }
    }
}