using AutoMapper;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Common.Pagination;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Articles.ArticleDTOs;
using Weblu.Application.DTOs.Articles.ArticleDTOs.ArticleImageDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Articles;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Services.Articles;
using Weblu.Application.Parameters.Articles;
using Weblu.Domain.Entities.Articles;

using Weblu.Domain.Errors.Articles;

namespace Weblu.Application.Services.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public ArticleService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IArticleCategoryRepository articleCategoryRepository,
            IArticleRepository articleRepository,
            ICommentRepository commentRepository,
            IDomainEventDispatcher domainEventDispatcher
            )

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _articleRepository = articleRepository;
            _articleCategoryRepository = articleCategoryRepository;
            _commentRepository = commentRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }
        public async Task<ArticleDetailDTO> CreateAsync(CreateArticleDTO createArticleDTO)
        {
            Article article = _mapper.Map<Article>(createArticleDTO);

            ArticleCategory articleCategory = await _articleCategoryRepository.GetByIdAsync(article.CategoryId) ?? throw new NotFoundException(ArticleCategoryErrorCodes.NotFound);
            article.Category = articleCategory;

            _articleRepository.Add(article);
            await _unitOfWork.CommitAsync();


            ArticleDetailDTO articleDetailDTO = _mapper.Map<ArticleDetailDTO>(article);
            return articleDetailDTO;
        }

        public async Task DeleteAsync(int articleId)
        {
            Article article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            if (article.IsPublished) throw new ConflictException(ArticleErrorCodes.IsPublish);

            article.Delete();
            await _unitOfWork.CommitAsync();
        }
        public async Task<List<ArticleSummaryDTO>> GetAllAsync(ArticleParameters articleParameters)
        {
            IReadOnlyList<Article> articles = await _articleRepository.GetAllAsync(articleParameters);
            var articleIds = articles.Select(a => a.Id).ToList();

            var commentCounts = await _commentRepository.GetCountByIdsAsync(articleIds);
            var likesCounts = await _articleRepository.GetLikeCountByIdsAsync(articleIds);


            var articleSummaryDTOs = _mapper.Map<List<ArticleSummaryDTO>>(articles)
                                     ?? new List<ArticleSummaryDTO>();

            foreach (var dTO in articleSummaryDTOs)
            {
                dTO.CommentCount = commentCounts.TryGetValue(dTO.Id, out var cCount) ? cCount : 0;
                dTO.LikeCount = likesCounts.TryGetValue(dTO.Id, out var lCount) ? lCount : 0;
            }

            return articleSummaryDTOs;
        }

        public async Task<PagedResponse<ArticleSummaryDTO>> GetAllPagedAsync(ArticleParameters articleParameters)
        {
            PagedList<Article> articles = await _articleRepository.GetAllAsync(articleParameters);
            var articleIds = articles.Select(a => a.Id).ToList();
            var commentCounts = await _commentRepository.GetCountByIdsAsync(articleIds);
            var likesCounts = await _articleRepository.GetLikeCountByIdsAsync(articleIds);

            List<ArticleSummaryDTO> articleSummaryDTOs = _mapper.Map<List<ArticleSummaryDTO>>(articles);
            foreach (var dTO in articleSummaryDTOs)
            {
                dTO.CommentCount = commentCounts.TryGetValue(dTO.Id, out var cCount) ? cCount : 0;
                dTO.LikeCount = likesCounts.TryGetValue(dTO.Id, out var lCount) ? lCount : 0;
            }

            var pagedResponse = _mapper.Map<PagedResponse<ArticleSummaryDTO>>(articles);
            pagedResponse.Items = articleSummaryDTOs;
            return pagedResponse;
        }

        public async Task<ArticleDetailDTO> GetByIdAsync(int articleId)
        {
            Article article = await _articleRepository.GetByIdWithImagesAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            List<ArticleImageDTO> imageDTOs = article.ArticleImages.Select(x => _mapper.Map<ArticleImageDTO>(x)).ToList();
            ArticleDetailDTO articleDetailDTO = _mapper.Map<ArticleDetailDTO>(article);
            articleDetailDTO.ArticleImages = imageDTOs;
            articleDetailDTO.CommentCount = await _commentRepository.GetCountAsync(articleId);
            articleDetailDTO.LikeCount = await _articleRepository.GetLikeCountAsync(articleId);
            return articleDetailDTO;
        }

        public async Task<ArticleDetailDTO> EditAsync(int articleId, UpdateArticleDTO updateArticleDTO)
        {
            Article article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            article = _mapper.Map(updateArticleDTO, article);

            ArticleCategory articleCategory = await _articleCategoryRepository.GetByIdAsync(updateArticleDTO.CategoryId) ?? throw new NotFoundException(ArticleCategoryErrorCodes.NotFound);
            article.Category = articleCategory;

            article.Update();
            _articleRepository.Update(article);
            await _unitOfWork.CommitAsync();

            await _domainEventDispatcher.DispatchAsync(article.Events);
            article.ClearEvents();

            ArticleDetailDTO articleDetailDTO = _mapper.Map<ArticleDetailDTO>(article);
            return articleDetailDTO;
        }

        public async Task ViewAsync(int articleId)
        {
            Article article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            article.IncreaseViewCount();
            _articleRepository.Update(article);
            await _unitOfWork.CommitAsync();
        }

        public async Task Publish(int articleId)
        {
            Article article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);

            article.Publish();
            await _domainEventDispatcher.DispatchAsync(article.Events);
            article.ClearEvents();

            await _unitOfWork.CommitAsync();
        }

        public async Task Unpublish(int articleId)
        {
            Article article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);

            article.Unpublish();

            await _domainEventDispatcher.DispatchAsync(article.Events);
            article.ClearEvents();

            await _unitOfWork.CommitAsync();
        }
    }
}