using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = context;

        private IServiceRepository? _serviceRepo;
        public IServiceRepository Services => _serviceRepo ??= new ServiceRepository(_context);

        private IFeatureRepository? _featureRepo;
        public IFeatureRepository Features => _featureRepo ??= new FeatureRepository(_context);

        private IMethodRepository? _methodRepository;
        public IMethodRepository Methods => _methodRepository ??= new MethodRepository(_context);

        private IImageRepository? _imageRepository;
        public IImageRepository Images => _imageRepository ??= new ImageRepository(_context);

        private IRefreshTokenRepository? _refreshTokenRepository;
        public IRefreshTokenRepository RefreshTokens => _refreshTokenRepository ??= new RefreshTokenRepository(_context);

        private IProfileImageRepository? _profileImageRepository;
        public IProfileImageRepository Profiles => _profileImageRepository ??= new ProfileImageRepository(_context);

        private IUserRepository? _userRepository;
        public IUserRepository Users => _userRepository ??= new UserRepository(_context);

        private IPortfolioCategoryRepository? _portfolioCategoryRepository;
        public IPortfolioCategoryRepository PortfolioCategories => _portfolioCategoryRepository ??= new PortfolioCategoryRepository(_context);

        private IPortfolioRepository? _portfolioRepository;
        public IPortfolioRepository Portfolios => _portfolioRepository ??= new PortfolioRepository(_context);

        private IContributorRepository? _contributorRepository;
        public IContributorRepository Contributors => _contributorRepository ??= new ContributorRepository(_context);

        private ITicketRepository? _ticketRepository;
        public ITicketRepository Tickets => _ticketRepository ??= new TicketRepository(_context);

        private ITicketMessageRepository? _ticketMessageRepository;
        public ITicketMessageRepository TicketMessages => _ticketMessageRepository ??= new TicketMessageRepository(_context);

        private IFaqRepository? _faqRepository;
        public IFaqRepository Faqs => _faqRepository ??= new FaqRepository(_context);

        private IFaqCategoryRepository? _faqCategoryRepository;
        public IFaqCategoryRepository FaqCategories => _faqCategoryRepository ??= new FaqCategoryRepository(_context);

        private IFavoriteListRepository? _favoriteListRepository;
        public IFavoriteListRepository FavoriteLists => _favoriteListRepository ??= new FavoriteListRepository(_context);

        private IUserFavoritesRepository? _favoritesRepository;
        public IUserFavoritesRepository UserFavorites => _favoritesRepository ??= new UserFavoritesRepository(_context);

        private IAboutUsRepository? _aboutUsRepository;
        public IAboutUsRepository AboutUs => _aboutUsRepository ??= new AboutUsRepository(_context);

        private ISocialMediaRepository? _socialMediaRepository;
        public ISocialMediaRepository SocialMedias => _socialMediaRepository ??= new SocialMediaRepository(_context);
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}