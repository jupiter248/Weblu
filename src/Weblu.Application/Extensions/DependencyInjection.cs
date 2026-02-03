using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Weblu.Application.Dtos.FavoriteListDtos;
using Weblu.Application.EventHandlers;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Interfaces.Services.Articles;
using Weblu.Application.Interfaces.Services.Portfolios;
using Weblu.Application.Interfaces.Services.ServiceServices;
using Weblu.Application.Interfaces.Services.Users.UserFavorites;
using Weblu.Application.Interfaces.Services.Users.UserFavorites.FavoriteLists;
using Weblu.Application.Mappers;
using Weblu.Application.Services;
using Weblu.Application.Services.Articles;
using Weblu.Application.Services.FavoriteLists;
using Weblu.Application.Services.Portfolios;
using Weblu.Application.Services.ServiceServices;
using Weblu.Application.Services.UserFavorites;
using Weblu.Application.Services.UserFavorites.FavoriteLists;
using Weblu.Domain.Events.Articles;
using Weblu.Domain.Interfaces;

namespace Weblu.Application.Extensions
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {

            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<IMethodService, MethodService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IProfileImageService, ProfileImageService>();
            services.AddScoped<IContributorService, ContributorService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ITicketMessageService, TicketMessageService>();
            services.AddScoped<IFaqService, FaqService>();
            services.AddScoped<IFaqCategoryService, FaqCategoryService>();
            services.AddScoped<IFavoriteListService, FavoriteListService>();
            services.AddScoped<IFavoriteListPortfolioService, FavoriteListPortfolioService>();
            services.AddScoped<IFavoriteListArticleService, FavoriteListArticleService>();
            services.AddScoped<IUserArticleFavoriteService, UserArticleFavoriteService>();
            services.AddScoped<IUserPortfolioFavoriteService, UserPortfolioFavoriteService>();
            services.AddScoped<IAboutUsService, AboutUsService>();
            services.AddScoped<ISocialMediaService, SocialMediaService>();
            services.AddScoped<IArticleCategoryService, ArticleCategoryService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ISearchService, SearchService>();
            // Services
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IServiceFeatureService, ServiceFeatureService>();
            services.AddScoped<IServiceMethodService, ServiceMethodService>();
            services.AddScoped<IServiceImageService, ServiceImageService>();
            // Articles
            services.AddScoped<IArticleService, ArticleService>();
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

            //Events
            services.AddScoped<IDomainEventHandler<ArticleAddedEvent>, UpdateSearchIndexHandler>();

        }
    }
}