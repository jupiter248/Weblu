using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            List<ArticleSummaryDto> articleSummaryDtos = _mapper.Map<List<ArticleSummaryDto>>(articles);
            foreach (ArticleSummaryDto articleSummaryDto in articleSummaryDtos)
            {
                articleSummaryDto.CommentCount = await _commentRepository.GetCountAsync(articleSummaryDto.Id);
                articleSummaryDto.LikeCount = await _articleRepository.GetLikeCountAsync(articleSummaryDto.Id);
            }
            return articleSummaryDtos;
        }

        public async Task<PagedResponse<ArticleSummaryDto>> GetAllPagedArticlesAsync(ArticleParameters articleParameters)
        {
            PagedList<Article> articles = await _articleRepository.GetAllAsync(articleParameters);
            List<ArticleSummaryDto> articleSummaryDtos = _mapper.Map<List<ArticleSummaryDto>>(articles);
            foreach (ArticleSummaryDto articleSummaryDto in articleSummaryDtos)
            {
                articleSummaryDto.CommentCount = await _commentRepository.GetCountAsync(articleSummaryDto.Id);
                articleSummaryDto.LikeCount = await _articleRepository.GetLikeCountAsync(articleSummaryDto.Id);
            }
            var pagedResponse = _mapper.Map<PagedResponse<ArticleSummaryDto>>(articles);
            pagedResponse.Items = articleSummaryDtos;
            return pagedResponse;
        }

        public async Task<ArticleDetailDto> GetArticleByIdAsync(int articleId)
        {
            Article article = await _articleRepository.GetByIdWithImagesAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            List<ArticleImageDto> imageDtos = new List<ArticleImageDto>();
            foreach (ArticleImage item in article.ArticleImages)
            {
                imageDtos.Add(_mapper.Map<ArticleImageDto>(item));
            }
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

            if (article.IsPublished)
            {
                if (article.PublishedAt == DateTimeOffset.MinValue)
                {
                    article.PublishedAt = DateTimeOffset.Now;
                }
            }
            else if (!article.IsPublished)
            {
                article.PublishedAt = DateTimeOffset.MinValue;
            }

            _articleRepository.Update(article);
            await _unitOfWork.CommitAsync();

            ArticleDetailDto articleDetailDto = _mapper.Map<ArticleDetailDto>(article);
            return articleDetailDto;
        }

        public async Task ViewArticleAsync(int articleId)
        {
            Article article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            article.ViewCount++;

            _articleRepository.Update(article);
            await _unitOfWork.CommitAsync();
        }
    }
}