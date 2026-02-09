using AutoMapper;
using Weblu.Application.Dtos.Portfolios.PortfolioDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Portfolios;
using Weblu.Application.Interfaces.Repositories.Users.UserFavorites;
using Weblu.Application.Interfaces.Services.Users.Favorites;
using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Entities.Users.Favorites;
using Weblu.Domain.Errors.Portfolios;
using Weblu.Domain.Errors.Users.Favorites;

namespace Weblu.Application.Services.Users.Favorites
{
    public class UserPortfolioFavoriteService : IUserPortfolioFavoriteService
    {
        private readonly IMapper _mapper;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IUserPortfolioFavoriteRepository _userPortfolioFavoriteRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserPortfolioFavoriteService(IMapper mapper, IUnitOfWork unitOfWork, IPortfolioRepository portfolioRepository, IUserPortfolioFavoriteRepository userFavoritesRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _portfolioRepository = portfolioRepository;
            _userPortfolioFavoriteRepository = userFavoritesRepository;
        }

        public async Task AddAsync(string userId, int portfolioId)
        {
            Portfolio? portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            bool alreadyAdded = await _userPortfolioFavoriteRepository.IsFavoriteAsync(userId, portfolioId);
            if (alreadyAdded)
            {
                throw new ConflictException(FavoriteErrorCodes.PortfolioAlreadyAddedToFavorites);
            }

            FavoritePortfolio favoritePortfolio = new FavoritePortfolio()
            {
                PortfolioId = portfolio.Id,
                Portfolio = portfolio,
                UserId = userId
            };

            await _userPortfolioFavoriteRepository.AddAsync(favoritePortfolio);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(string userId, int portfolioId)
        {
            bool exists = await _userPortfolioFavoriteRepository.IsFavoriteAsync(userId, portfolioId);
            if (exists == false)
            {
                throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            }
            await _userPortfolioFavoriteRepository.DeleteAsync(userId, portfolioId);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<PortfolioSummaryDto>> GetAllAsync(string userId, FavoriteParameters favoriteParameters)
        {
            IReadOnlyList<FavoritePortfolio> favoritePortfolios = await _userPortfolioFavoriteRepository.GetAllAsync(userId, favoriteParameters);
            List<Portfolio> portfolios = favoritePortfolios.Select(x => x.Portfolio).ToList();
            List<PortfolioSummaryDto> portfolioSummaryDtos = _mapper.Map<List<PortfolioSummaryDto>>(portfolios) ?? default!;
            return portfolioSummaryDtos;
        }

        public async Task<bool> IsFavoriteAsync(string userId, int portfolioId)
        {
            bool isFavorite = await _userPortfolioFavoriteRepository.IsFavoriteAsync(userId, portfolioId);
            return isFavorite;
        }
    }
}