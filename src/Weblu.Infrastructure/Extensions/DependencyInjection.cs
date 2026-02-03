using Microsoft.Extensions.DependencyInjection;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Users;
using Weblu.Application.Interfaces.Repositories.Users.UserFavorites;
using Weblu.Application.Interfaces.Services.Users;
using Weblu.Application.Interfaces.Services.Users.UserFavorites;
using Weblu.Application.Mappers;
using Weblu.Application.Services.Interfaces;
using Weblu.Application.Services.UserFavorites;
using Weblu.Infrastructure.Common.Services;
using Weblu.Infrastructure.EventDispatching;
using Weblu.Infrastructure.Identity.Mappers;
using Weblu.Infrastructure.Identity.Services;
using Weblu.Infrastructure.Localization;
using Weblu.Infrastructure.Logger;
using Weblu.Infrastructure.Repositories;
using Weblu.Infrastructure.Repositories.Users;
using Weblu.Infrastructure.Repositories.Users.UserFavorites;
using Weblu.Infrastructure.Token;

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
            services.AddScoped<IFaqRepository, FaqRepository>();
            services.AddScoped<IFaqCategoryRepository, FaqCategoryRepository>();
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

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserPortfolioFavoriteService, UserPortfolioFavoriteService>();
            services.AddScoped<IUserArticleFavoriteService, UserArticleFavoriteService>();
            services.AddSingleton<IErrorService, ErrorService>();
            services.AddSingleton<IFilePathProvider, FilePathProvider>();

            // Mappers
            services.AddScoped(typeof(IAppLogger<>), typeof(AppLoggerService<>));
            services.AddAutoMapper(typeof(UserProfile));
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
            services.AddAutoMapper(typeof(SearchProfile));

            // DisPatcher
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        }
    }
}