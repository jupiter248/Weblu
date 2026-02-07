using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories.Articles;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Repositories.Users;
using Weblu.Application.Interfaces.Repositories.Users.UserFavorites;
using Weblu.Application.Interfaces.Services.Users.UserFavorites.FavoriteLists;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Users.Favorites;
using Weblu.Domain.Errors.Articles;
using Weblu.Domain.Errors.Users;
using Weblu.Domain.Errors.Users.Favorites;

namespace Weblu.Application.Services.Users.Favorites.FavoriteLists
{
    public class FavoriteListArticleService : IFavoriteListArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserArticleFavoriteRepository _userArticleFavoriteRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFavoriteListRepository _favoriteListRepository;

        public FavoriteListArticleService(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IFavoriteListRepository favoriteListRepository,
            IArticleRepository articleRepository,
            IUserArticleFavoriteRepository userArticleFavoriteRepository
            )
        {
            _favoriteListRepository = favoriteListRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _userArticleFavoriteRepository = userArticleFavoriteRepository;
            _articleRepository = articleRepository;
        }
        public async Task AddAsync(string userId, int favoriteListId, int articleId)
        {
            bool userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            FavoriteList favoriteList = await _favoriteListRepository.GetByUserAndListIdAsync(userId, favoriteListId) ?? throw new NotFoundException(FavoriteListErrorCodes.NotFound);
            Article article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            FavoriteArticle favoriteArticle = await _userArticleFavoriteRepository.GetByArticleIdAsync(userId, article.Id) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);

            favoriteList.AddFavoriteArticle(favoriteArticle);
            await _unitOfWork.CommitAsync();
        }
        public async Task DeleteAsync(string userId, int favoriteListId, int articleId)
        {
            bool userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            FavoriteList favoriteList = await _favoriteListRepository.GetByUserAndListIdAsync(userId, favoriteListId) ?? throw new NotFoundException(FavoriteListErrorCodes.NotFound);
            Article article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            FavoriteArticle favoriteArticle = await _userArticleFavoriteRepository.GetByArticleIdAsync(userId, article.Id) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);

            favoriteList.DeleteFavoriteArticle(favoriteArticle);
            await _unitOfWork.CommitAsync();
        }
    }
}