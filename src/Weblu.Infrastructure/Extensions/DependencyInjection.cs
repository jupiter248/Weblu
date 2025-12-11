using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Infrastructure.Identity.Mappers;
using Weblu.Infrastructure.Identity.Services;
using Weblu.Infrastructure.Localization;
using Weblu.Infrastructure.Logger;
using Weblu.Infrastructure.Repositories;
using Weblu.Infrastructure.Token;

namespace Weblu.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            // services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();
            services.AddScoped<IMethodRepository, MethodRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IProfileImageRepository, ProfileImageRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPortfolioCategoryRepository, PortfolioCategoryRepository>();
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            services.AddScoped<IContributorRepository, ContributorRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketMessageRepository, TicketMessageRepository>();
            services.AddScoped<IFaqRepository, FaqRepository>();
            services.AddScoped<IFaqCategoryRepository, FaqCategoryRepository>();
            services.AddScoped<IFavoriteListRepository, FavoriteListRepository>();
            services.AddScoped<IUserFavoritesRepository, UserFavoritesRepository>();
            services.AddScoped<IAboutUsRepository, AboutUsRepository>();
            services.AddScoped<ISocialMediaRepository, SocialMediaRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ITagRepository, TagRepository>();













            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserFavoriteService, UserFavoritesService>();


            services.AddSingleton<IErrorService, ErrorService>();

            services.AddScoped(typeof(IAppLogger<>), typeof(AppLoggerService<>));
            services.AddAutoMapper(typeof(UserProfile));
        }
    }
}