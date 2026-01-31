using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Users;
using Weblu.Application.Interfaces.Services.Articles;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Errors.Articles;
using Weblu.Domain.Errors.Users;

namespace Weblu.Application.Services.Articles
{
    public class ArticleLikeService : IArticleLikeService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArticleRepository _articleRepository;

        public ArticleLikeService(IUserRepository userRepository, IUnitOfWork unitOfWork, IArticleRepository articleRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _articleRepository = articleRepository;
        }
        public async Task LikeAsync(int articleId, string userId)
        {
            Article article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            bool userExists = await _userRepository.UserExistsAsync(userId);

            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }

            ArticleLike articleLike = new ArticleLike()
            {
                ArticleId = articleId,
                UserId = userId,
                Article = article,
            };

            await _articleRepository.LoadLikesAsync(article);

            article.Like(articleLike);
            await _unitOfWork.CommitAsync();
        }
        public async Task UnlikeAsync(int articleId, string userId)
        {
            Article article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            bool userExists = await _userRepository.UserExistsAsync(userId);

            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }

            await _articleRepository.LoadLikesAsync(article);

            article.UnLike(userId);
            await _unitOfWork.CommitAsync();
        }
    }
}