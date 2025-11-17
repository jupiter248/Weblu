using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Dtos.FavoriteDtos;
using Weblu.Application.Dtos.PortfolioDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Favorites;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Errors.Favorites;
using Weblu.Domain.Errors.Portfolios;
using Weblu.Domain.Errors.Users;
using Weblu.Infrastructure.Identity.Entities;

namespace Weblu.Infrastructure.Identity.Services
{
    public class UserFavoritesService : IUserFavoriteService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UserFavoritesService(UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddPortfolioToFavorite(string userId, int portfolioId)
        {
            AppUser? user = await _userManager.Users.Include(f => f.FavoritePortfolios).FirstOrDefaultAsync(u => u.Id == userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            Portfolio? portfolio = await _unitOfWork.Portfolios.GetPortfolioByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);

            if (user.FavoritePortfolios.Any(p => p.PortfolioId == portfolio.Id))
            {
                throw new ConflictException(FavoriteErrorCodes.PortfolioAlreadyAddedToFavorites);
            }

            FavoritePortfolio favoritePortfolio = new FavoritePortfolio()
            {
                PortfolioId = portfolio.Id,
                Portfolio = portfolio,
                UserId = userId
            };

            user.FavoritePortfolios.Add(favoritePortfolio);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeletePortfolioFromFavorite(string userId, int portfolioId)
        {
            AppUser? user = await _userManager.Users.Include(f => f.FavoritePortfolios).FirstOrDefaultAsync(u => u.Id == userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            Portfolio? portfolio = await _unitOfWork.Portfolios.GetPortfolioByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);

            FavoritePortfolio? favoritePortfolio = user.FavoritePortfolios.FirstOrDefault(p => p.PortfolioId == portfolio.Id);
            if (favoritePortfolio == null)
            {
                throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            }

            user.FavoritePortfolios.Remove(favoritePortfolio);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<PortfolioSummaryDto>> GetAllFavoritePortfoliosAsync(string userId, FavoriteParameters favoriteParameters)
        {
            List<FavoritePortfolio> favoritePortfolios = await _unitOfWork.UserFavorites.GetAllFavoritePortfoliosAsync(userId, favoriteParameters);
            List<Portfolio> portfolios = new List<Portfolio>();
            foreach (FavoritePortfolio item in favoritePortfolios)
            {
                portfolios.Add(item.Portfolio);
            }
            List<PortfolioSummaryDto> portfolioSummaryDtos = _mapper.Map<List<PortfolioSummaryDto>>(portfolios);
            return portfolioSummaryDtos;
        }

        public async Task<bool> IsFavorite(string userId, int portfolioId)
        {
            AppUser? user = await _userManager.Users.Include(f => f.FavoritePortfolios).FirstOrDefaultAsync(u => u.Id == userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            Portfolio? portfolio = await _unitOfWork.Portfolios.GetPortfolioByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);

            if (!user.FavoritePortfolios.Any(p => p.PortfolioId == portfolio.Id))
            {
                return false;
            }
            return true;
        }
    }
}