using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IServiceRepository Services { get; }
        IFeatureRepository Features { get; }
        IMethodRepository Methods { get; }
        IImageRepository Images { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        IProfileImageRepository Profiles { get; }
        IUserRepository Users { get; }
        IPortfolioCategoryRepository PortfolioCategories { get; }
        IPortfolioRepository Portfolios { get; }
        IContributorRepository Contributors { get; }
        ITicketRepository Tickets { get; }
        ITicketMessageRepository TicketMessages { get; }
        IFaqRepository Faqs { get; }
        IFaqCategoryRepository FaqCategories { get; }
        IFavoriteListRepository FavoriteLists { get; }
        IUserFavoritesRepository UserFavorites { get; }
        IAboutUsRepository AboutUs { get; }
        ISocialMediaRepository SocialMedias { get; }
        IArticleRepository Articles { get; }
        IArticleCategoryRepository ArticleCategories { get; }
        ICommentRepository Comments { get; }


        Task<int> CommitAsync();
    }
}