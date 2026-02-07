using AutoMapper;
using Weblu.Application.Dtos.Articles.ArticleDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories.Articles;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Repositories.Users.UserFavorites;
using Weblu.Application.Interfaces.Services.Users.UserFavorites;
using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Users.Favorites;
using Weblu.Domain.Errors.Articles;
using Weblu.Domain.Errors.Users.Favorites;

namespace Weblu.Application.Services.Users.Favorites
{
    public class UserArticleFavoriteService : IUserArticleFavoriteService
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;
        private readonly IUserArticleFavoriteRepository _userArticleFavoriteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentRepository _commentRepository;
        public UserArticleFavoriteService(ICommentRepository commentRepository, IMapper mapper, IUnitOfWork unitOfWork, IArticleRepository articleRepository, IUserArticleFavoriteRepository userArticleFavoriteRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _articleRepository = articleRepository;
            _userArticleFavoriteRepository = userArticleFavoriteRepository;
            _commentRepository = commentRepository;
        }

        public async Task AddAsync(string userId, int articleId)
        {
            Article? article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            bool alreadyAdded = await _userArticleFavoriteRepository.IsFavoriteAsync(userId, articleId);
            if (alreadyAdded)
            {
                throw new ConflictException(FavoriteErrorCodes.ArticleAlreadyAddedToFavorites);
            }

            FavoriteArticle favoriteArticle = new FavoriteArticle()
            {
                ArticleId = article.Id,
                Article = article,
                UserId = userId
            };

            await _userArticleFavoriteRepository.AddAsync(favoriteArticle);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(string userId, int articleId)
        {
            bool exists = await _userArticleFavoriteRepository.IsFavoriteAsync(userId, articleId);
            if (exists == false)
            {
                throw new NotFoundException(ArticleErrorCodes.NotFound);
            }
            await _userArticleFavoriteRepository.DeleteAsync(userId, articleId);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ArticleSummaryDto>> GetAllAsync(string userId, FavoriteParameters favoriteParameters)
        {
            IEnumerable<FavoriteArticle> favoriteArticles = await _userArticleFavoriteRepository.GetAllAsync(userId, favoriteParameters);
            List<Article> articles = favoriteArticles.Select(x => x.Article).ToList();
            var articleIds = articles.Select(a => a.Id).ToList();

            var commentCounts = await _commentRepository.GetCountByIdsAsync(articleIds);
            var likesCounts = await _articleRepository.GetLikeCountByIdsAsync(articleIds);


            List<ArticleSummaryDto> articleSummaryDtos = _mapper.Map<List<ArticleSummaryDto>>(articles) ?? default!;

            foreach (var dto in articleSummaryDtos)
            {
                dto.CommentCount = commentCounts.TryGetValue(dto.Id, out var cCount) ? cCount : 0;
                dto.LikeCount = likesCounts.TryGetValue(dto.Id, out var lCount) ? lCount : 0;
            }
            return articleSummaryDtos;
        }

        public async Task<bool> IsFavoriteAsync(string userId, int articleId)
        {
            bool isFavorite = await _userArticleFavoriteRepository.IsFavoriteAsync(userId, articleId);
            return isFavorite;
        }
    }
}