using AutoMapper;
using Weblu.Application.Dtos.ArticleDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Users.UserFavorites;
using Weblu.Application.Interfaces.Services.Users.UserFavorites;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Favorites;
using Weblu.Domain.Errors.Articles;
using Weblu.Domain.Errors.Favorites;

namespace Weblu.Application.Services.UserFavorites
{
    public class UserArticleFavoriteService : IUserArticleFavoriteService
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;
        private readonly IUserArticleFavoriteRepository _userArticleFavoriteRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserArticleFavoriteService(IMapper mapper, IUnitOfWork unitOfWork, IArticleRepository articleRepository, IUserArticleFavoriteRepository userArticleFavoriteRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _articleRepository = articleRepository;
            _userArticleFavoriteRepository = userArticleFavoriteRepository;
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
            List<Article> articles = new List<Article>();
            foreach (FavoriteArticle item in favoriteArticles)
            {
                articles.Add(item.Article);
            }
            List<ArticleSummaryDto> articleSummaryDtos = _mapper.Map<List<ArticleSummaryDto>>(articles) ?? default!;
            return articleSummaryDtos;
        }

        public async Task<bool> IsFavoriteAsync(string userId, int articleId)
        {
            bool isFavorite = await _userArticleFavoriteRepository.IsFavoriteAsync(userId, articleId);
            return isFavorite;
        }
    }
}