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
        private IFeatureRepository? _featureRepo;
        private IMethodRepository? _methodRepository;
        private IImageRepository? _imageRepository;
        private IRefreshTokenRepository? _refreshTokenRepository;
        private IProfileImageRepository? _profileImageRepository;
        private IUserRepository? _userRepository;
        private IPortfolioCategoryRepository? _portfolioCategoryRepository;
        private IPortfolioRepository? _portfolioRepository;
        private IContributorRepository? _contributorRepository;
        private ITicketRepository? _ticketRepository;
        private ITicketMessageRepository? _ticketMessageRepository;
        private IFaqRepository? _faqRepository;
        private IFaqCategoryRepository? _faqCategoryRepository;
        private IFavoriteListRepository? _favoriteListRepository;
        private IUserFavoritesRepository? _favoritesRepository;
        private IAboutUsRepository? _aboutUsRepository;
        private ISocialMediaRepository? _socialMediaRepository;
        private IArticleRepository? _articleRepository;
        private IArticleCategoryRepository? _articleCategoryRepository;






        public IServiceRepository Services => _serviceRepo ??= new ServiceRepository(_context);
        public IFeatureRepository Features => _featureRepo ??= new FeatureRepository(_context);
        public IMethodRepository Methods => _methodRepository ??= new MethodRepository(_context);
        public IImageRepository Images => _imageRepository ??= new ImageRepository(_context);
        public IRefreshTokenRepository RefreshTokens => _refreshTokenRepository ??= new RefreshTokenRepository(_context);
        public IProfileImageRepository Profiles => _profileImageRepository ??= new ProfileImageRepository(_context);
        public IUserRepository Users => _userRepository ??= new UserRepository(_context);
        public IPortfolioCategoryRepository PortfolioCategories => _portfolioCategoryRepository ??= new PortfolioCategoryRepository(_context);
        public IPortfolioRepository Portfolios => _portfolioRepository ??= new PortfolioRepository(_context);
        public IContributorRepository Contributors => _contributorRepository ??= new ContributorRepository(_context);
        public ITicketRepository Tickets => _ticketRepository ??= new TicketRepository(_context);
        public ITicketMessageRepository TicketMessages => _ticketMessageRepository ??= new TicketMessageRepository(_context);
        public IFaqRepository Faqs => _faqRepository ??= new FaqRepository(_context);
        public IFaqCategoryRepository FaqCategories => _faqCategoryRepository ??= new FaqCategoryRepository(_context);
        public IFavoriteListRepository FavoriteLists => _favoriteListRepository ??= new FavoriteListRepository(_context);
        public IUserFavoritesRepository UserFavorites => _favoritesRepository ??= new UserFavoritesRepository(_context);
        public IAboutUsRepository AboutUs => _aboutUsRepository ??= new AboutUsRepository(_context);
        public ISocialMediaRepository SocialMedias => _socialMediaRepository ??= new SocialMediaRepository(_context);
        public IArticleRepository Articles => _articleRepository ??= new ArticleRepository(_context);
        public IArticleCategoryRepository ArticleCategories => _articleCategoryRepository ??= new ArticleCategoryRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}