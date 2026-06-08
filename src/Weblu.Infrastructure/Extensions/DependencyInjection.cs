using Microsoft.Extensions.DependencyInjection;
using Weblu.Application.Common.Interfaces;
using Weblu.Domain.Interfaces.Repositories;
using Weblu.Domain.Interfaces.Repositories.About;
using Weblu.Domain.Interfaces.Repositories.Articles;
using Weblu.Domain.Interfaces.Repositories.Common;
using Weblu.Domain.Interfaces.Repositories.FAQs;
using Weblu.Domain.Interfaces.Repositories.Images;
using Weblu.Domain.Interfaces.Repositories.Portfolios;
using Weblu.Domain.Interfaces.Repositories.Services;
using Weblu.Domain.Interfaces.Repositories.Tickets;
using Weblu.Domain.Interfaces.Repositories.Users;
using Weblu.Domain.Interfaces.Repositories.Users.Favorites;
using Weblu.Domain.Interfaces.Repositories.Users.Roles;
using Weblu.Domain.Interfaces.Repositories.Users.Tokens;
using Weblu.Domain.Interfaces.Repositories.Users.UserFavorites;
using Weblu.Application.Interfaces.Services.Auth;
using Weblu.Application.Interfaces.Services.Users;
using Weblu.Application.Interfaces.Services.Users.Favorites;
using Weblu.Application.Mappers.About;
using Weblu.Application.Mappers.Common;
using Weblu.Application.Mappers.FAQs;
using Weblu.Application.Mappers.Images;
using Weblu.Application.Mappers.Portfolios;
using Weblu.Application.Mappers.Services;
using Weblu.Application.Mappers.Tickets;
using Weblu.Application.Mappers.Users;
using Weblu.Application.Services.Interfaces.Users.Tokens;
using Weblu.Application.Services.Users.Favorites;
using Weblu.Infrastructure.Common.Services;
using Weblu.Infrastructure.EventDispatching;
using Weblu.Infrastructure.Identity.Mappers;
using Weblu.Infrastructure.Identity.Services;
using Weblu.Infrastructure.Identity.Token;
using Weblu.Infrastructure.Localization;
using Weblu.Infrastructure.Logger;
using Weblu.Infrastructure.Repositories;
using Weblu.Infrastructure.Repositories.About;
using Weblu.Infrastructure.Repositories.Articles;
using Weblu.Infrastructure.Repositories.Common;
using Weblu.Infrastructure.Repositories.FAQs;
using Weblu.Infrastructure.Repositories.Images;
using Weblu.Infrastructure.Repositories.Portfolios;
using Weblu.Infrastructure.Repositories.Services;
using Weblu.Infrastructure.Repositories.Tickets;
using Weblu.Infrastructure.Repositories.Users;
using Weblu.Infrastructure.Repositories.Users.Tokens;
using Weblu.Infrastructure.Repositories.Users.UserFavorites;
using Weblu.Domain.Interfaces.Repositories.Orders;
using Weblu.Infrastructure.Repositories.Orders;

namespace Weblu.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            // services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            // Repositories
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
            services.AddScoped<IFAQRepository, FAQRepository>();
            services.AddScoped<IFAQCategoryRepository, FAQCategoryRepository>();
            services.AddScoped<IFavoriteListRepository, FavoriteListRepository>();
            services.AddScoped<IUserPortfolioFavoriteRepository, UserPortfolioFavoriteRepository>();
            services.AddScoped<IUserArticleFavoriteRepository, UserArticleFavoriteRepository>();
            services.AddScoped<IAboutUsRepository, AboutUsRepository>();
            services.AddScoped<ISocialMediaRepository, SocialMediaRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ISearchRepository, SearchRepository>();
            services.AddScoped<IOrderRepository , OrderRepository>();
            services.AddScoped<IOrderStatusRepository , OrderStatusRepository>();


            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserPortfolioFavoriteService, UserPortfolioFavoriteService>();
            services.AddScoped<IUserArticleFavoriteService, UserArticleFavoriteService>();
            services.AddSingleton<IErrorService, ErrorService>();
            services.AddSingleton<IFilePathProviderService, FilePathProviderService>();

            // Mappers
            services.AddScoped(typeof(IAppLogger<>), typeof(AppLoggerService<>));


            // DisPatcher
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        }
    }
}