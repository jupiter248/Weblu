using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Dtos.FavoriteListDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Interfaces.Services.Articles;
using Weblu.Application.Interfaces.Services.Portfolios;
using Weblu.Application.Interfaces.Services.ServiceServices;
using Weblu.Application.Mappers;
using Weblu.Application.Services;
using Weblu.Application.Services.Articles;
using Weblu.Application.Services.Portfolios;
using Weblu.Application.Services.ServiceServices;

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
            services.AddScoped<IAboutUsService, AboutUsService>();
            services.AddScoped<ISocialMediaService, SocialMediaService>();
            services.AddScoped<IArticleCategoryService, ArticleCategoryService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ITagService, TagService>();
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


















            services.AddAutoMapper(typeof(ServiceProfile));
            services.AddAutoMapper(typeof(FeatureProfile));
            services.AddAutoMapper(typeof(MethodProfile));
            services.AddAutoMapper(typeof(ImageProfile));
            services.AddAutoMapper(typeof(RefreshTokenProfile));
            services.AddAutoMapper(typeof(PortfolioProfile));
            services.AddAutoMapper(typeof(ContributorProfile));
            services.AddAutoMapper(typeof(TicketProfile));
            services.AddAutoMapper(typeof(TicketMessageProfile));
            services.AddAutoMapper(typeof(FaqProfile));
            services.AddAutoMapper(typeof(FavoriteListProfile));
            services.AddAutoMapper(typeof(FavoriteProfile));
            services.AddAutoMapper(typeof(AboutUsProfile));
            services.AddAutoMapper(typeof(SocialMediaProfile));
            services.AddAutoMapper(typeof(TagProfile));











        }
    }
}