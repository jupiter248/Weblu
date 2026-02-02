using AutoMapper;
using Weblu.Application.Common.Pagination;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.ArticleDtos;
using Weblu.Application.Dtos.ArticleDtos.ArticleImageDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services.Articles;
using Weblu.Application.Parameters;
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

        public ArticleService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IArticleCategoryRepository articleCategoryRepository,
            IArticleRepository articleRepository,
            ICommentRepository commentRepository
            )

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _articleRepository = articleRepository;
            _articleCategoryRepository = articleCategoryRepository;
            _commentRepository = commentRepository;
        }
        public async Task<ArticleDetailDto> AddArticleAsync(AddArticleDto addArticleDto)
        {
            Article article = _mapper.Map<Article>(addArticleDto);

            ArticleCategory articleCategory = await _articleCategoryRepository.GetByIdAsync(article.CategoryId) ?? throw new NotFoundException(ArticleCategoryErrorCodes.NotFound);
            article.Category = articleCategory;

            _articleRepository.Add(article);
            await _unitOfWork.CommitAsync();

            ArticleDetailDto articleDetailDto = _mapper.Map<ArticleDetailDto>(article);
            return articleDetailDto;
        }

        public async Task DeleteArticleAsync(int articleId)
        {
            Article article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);

            _articleRepository.Delete(article);
            await _unitOfWork.CommitAsync();
        }
        public async Task<List<ArticleSummaryDto>> GetAllArticlesAsync(ArticleParameters articleParameters)
        {
            IReadOnlyList<Article> articles = await _articleRepository.GetAllAsync(articleParameters);
            var articleIds = articles.Select(a => a.Id).ToList();

            var commentCounts = await _commentRepository.GetCountByIdsAsync(articleIds);
            var likesCounts = await _articleRepository.GetLikeCountByIdsAsync(articleIds);


            var articleSummaryDtos = _mapper.Map<List<ArticleSummaryDto>>(articles)
                                     ?? new List<ArticleSummaryDto>();

            foreach (var dto in articleSummaryDtos)
            {
                dto.CommentCount = commentCounts.TryGetValue(dto.Id, out var cCount) ? cCount : 0;
                dto.LikeCount = likesCounts.TryGetValue(dto.Id, out var lCount) ? lCount : 0;
            }

            return articleSummaryDtos;
        }

        public async Task<PagedResponse<ArticleSummaryDto>> GetAllPagedArticlesAsync(ArticleParameters articleParameters)
        {
            PagedList<Article> articles = await _articleRepository.GetAllAsync(articleParameters);
            var articleIds = articles.Select(a => a.Id).ToList();
            var commentCounts = await _commentRepository.GetCountByIdsAsync(articleIds);
            var likesCounts = await _articleRepository.GetLikeCountByIdsAsync(articleIds);

            List<ArticleSummaryDto> articleSummaryDtos = _mapper.Map<List<ArticleSummaryDto>>(articles);
            foreach (var dto in articleSummaryDtos)
            {
                dto.CommentCount = commentCounts.TryGetValue(dto.Id, out var cCount) ? cCount : 0;
                dto.LikeCount = likesCounts.TryGetValue(dto.Id, out var lCount) ? lCount : 0;
            }

            var pagedResponse = _mapper.Map<PagedResponse<ArticleSummaryDto>>(articles);
            pagedResponse.Items = articleSummaryDtos;
            return pagedResponse;
        }

        public async Task<ArticleDetailDto> GetArticleByIdAsync(int articleId)
        {
            Article article = await _articleRepository.GetByIdWithImagesAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            List<ArticleImageDto> imageDtos = article.ArticleImages.Select(x => _mapper.Map<ArticleImageDto>(x)).ToList();
            ArticleDetailDto articleDetailDto = _mapper.Map<ArticleDetailDto>(article);
            articleDetailDto.ArticleImages = imageDtos;
            articleDetailDto.CommentCount = await _commentRepository.GetCountAsync(articleId);
            articleDetailDto.LikeCount = await _articleRepository.GetLikeCountAsync(articleId);
            return articleDetailDto;
        }

        public async Task<ArticleDetailDto> UpdateArticleAsync(int articleId, UpdateArticleDto updateArticleDto)
        {
            Article article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            article = _mapper.Map(updateArticleDto, article);

            ArticleCategory articleCategory = await _articleCategoryRepository.GetByIdAsync(updateArticleDto.CategoryId) ?? throw new NotFoundException(ArticleCategoryErrorCodes.NotFound);
            article.Category = articleCategory;

            article.UpdatePublishedStatus(updateArticleDto.IsPublished);

            _articleRepository.Update(article);
            await _unitOfWork.CommitAsync();

            ArticleDetailDto articleDetailDto = _mapper.Map<ArticleDetailDto>(article);
            return articleDetailDto;
        }

        public async Task ViewArticleAsync(int articleId)
        {
            Article article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            article.UpdateViewCount();
            _articleRepository.Update(article);
            await _unitOfWork.CommitAsync();
        }
    }
}