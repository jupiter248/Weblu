using Microsoft.Extensions.DependencyInjection;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Interfaces.Repositories.About;
using Weblu.Application.Interfaces.Repositories.Articles;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Repositories.FAQs;
using Weblu.Application.Interfaces.Repositories.Images;
using Weblu.Application.Interfaces.Repositories.Portfolios;
using Weblu.Application.Interfaces.Repositories.Services;
using Weblu.Application.Interfaces.Repositories.Tickets;
using Weblu.Application.Interfaces.Repositories.Users;
using Weblu.Application.Interfaces.Repositories.Users.Tokens;
using Weblu.Application.Interfaces.Repositories.Users.UserFavorites;
using Weblu.Application.Interfaces.Services.Users;
using Weblu.Application.Interfaces.Services.Users.UserFavorites;
using Weblu.Application.Mappers.About;
using Weblu.Application.Mappers.Common;
using Weblu.Application.Mappers.FAQs;
using Weblu.Application.Mappers.Images;
using Weblu.Application.Mappers.Portfolios;
using Weblu.Application.Mappers.Services;
using Weblu.Application.Mappers.Tickets;
using Weblu.Application.Mappers.Tokens;
using Weblu.Application.Mappers.Users;
using Weblu.Application.Services.Interfaces.Auth;
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