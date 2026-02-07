using Microsoft.Extensions.DependencyInjection;
using Weblu.Application.EventHandlers.Articles;
using Weblu.Application.EventHandlers.Portfolios;
using Weblu.Application.Interfaces.Services.About;
using Weblu.Application.Interfaces.Services.Articles;
using Weblu.Application.Interfaces.Services.Common;
using Weblu.Application.Interfaces.Services.FAQs;
using Weblu.Application.Interfaces.Services.Images;
using Weblu.Application.Interfaces.Services.Portfolios;
using Weblu.Application.Interfaces.Services.ServiceServices;
using Weblu.Application.Interfaces.Services.Tickets;
using Weblu.Application.Interfaces.Services.Users.Tokens;
using Weblu.Application.Interfaces.Services.Users.UserFavorites;
using Weblu.Application.Interfaces.Services.Users.UserFavorites.FavoriteLists;
using Weblu.Application.Services;
using Weblu.Application.Services.About;
using Weblu.Application.Services.Articles;
using Weblu.Application.Services.Common;
using Weblu.Application.Services.FAQs;
using Weblu.Application.Services.Images;
using Weblu.Application.Services.Portfolios;
using Weblu.Application.Services.ServiceServices;
using Weblu.Application.Services.Tickets;
using Weblu.Application.Services.Users.Favorites;
using Weblu.Application.Services.Users.Favorites.FavoriteLists;
using Weblu.Application.Services.Users.Tokens;
using Weblu.Domain.Events.Articles;
using Weblu.Domain.Events.Portfolios;
using Weblu.Domain.Interfaces;

namespace Weblu.Application.Extensions
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            // Images
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IProfileImageService, ProfileImageService>();
            // Tickets and Tokens
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ITicketMessageService, TicketMessageService>();
            // About Us
            services.AddScoped<IAboutUsService, AboutUsService>();
            services.AddScoped<ISocialMediaService, SocialMediaService>();
            // Favorites
            services.AddScoped<IUserArticleFavoriteService, UserArticleFavoriteService>();
            services.AddScoped<IFavoriteListService, FavoriteListService>();
            services.AddScoped<IFavoriteListPortfolioService, FavoriteListPortfolioService>();
            services.AddScoped<IUserPortfolioFavoriteService, UserPortfolioFavoriteService>();
            services.AddScoped<IFavoriteListArticleService, FavoriteListArticleService>();
            // FAQs
            services.AddScoped<IFaqService, FaqService>();
            services.AddScoped<IFaqCategoryService, FaqCategoryService>();
            // Common
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IContributorService, ContributorService>();
            services.AddScoped<IMethodService, MethodService>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<ITagService, TagService>();
            // Services
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IServiceFeatureService, ServiceFeatureService>();
            services.AddScoped<IServiceMethodService, ServiceMethodService>();
            services.AddScoped<IServiceImageService, ServiceImageService>();
            // Articles
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IArticleCategoryService, ArticleCategoryService>();
            services.AddScoped<IArticleContributorService, ArticleContributorService>();
            services.AddScoped<IArticleTagService, ArticleTagService>();
            services.AddScoped<IArticleLikeService, ArticleLikeService>();
            services.AddScoped<IArticleImageService, ArticleImageService>();
            // Portfolios
            services.AddScoped<IPortfolioCategoryService, PortfolioCategoryService>();
            services.AddScoped<IPortfolioService, PortfolioService>();
            services.AddScoped<IPortfolioFeatureService, PortfolioFeatureService>();
            services.AddScoped<IPortfolioMethodService, PortfolioMethodService>();
            services.AddScoped<IPortfolioImageService, PortfolioImageService>();
            services.AddScoped<IPortfolioContributorService, PortfolioContributorService>();

            //Event Articles
            services.AddScoped<IDomainEventHandler<ArticleAddedEvent>, ArticleSearchIndexHandler>();
            services.AddScoped<IDomainEventHandler<ArticleUpdatedEvent>, ArticleSearchUpdateHandler>();
            services.AddScoped<IDomainEventHandler<ArticleDeletedEvent>, ArticleSearchDeleteHandler>();
            //Event Portfolios
            services.AddScoped<IDomainEventHandler<PortfolioAddedEvent>, PortfolioSearchIndexHandler>();
            services.AddScoped<IDomainEventHandler<PortfolioUpdatedEvent>, PortfolioSearchUpdateHandler>();
            services.AddScoped<IDomainEventHandler<PortfolioDeletedEvent>, PortfolioSearchDeleteHandler>();


        }
    }
}