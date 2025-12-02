using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.ArticleDtos;
using Weblu.Application.Dtos.ArticleDtos.ArticleImageDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Mappers;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Errors.Articles;
using Weblu.Domain.Errors.Contributors;
using Weblu.Domain.Errors.Images;
using Weblu.Domain.Errors.Portfolios;
using Weblu.Domain.Errors.Tags;
using Weblu.Domain.Errors.Users;

namespace Weblu.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, ITagRepository tagRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tagRepository = tagRepository;

        }
        public async Task<ArticleDetailDto> AddArticleAsync(AddArticleDto addArticleDto)
        {
            Article article = _mapper.Map<Article>(addArticleDto);

            ArticleCategory articleCategory = await _unitOfWork.ArticleCategories.GetArticleCategoryByIdAsync(article.CategoryId) ?? throw new NotFoundException(ArticleCategoryErrorCodes.NotFound);
            article.Category = articleCategory;

            await _unitOfWork.Articles.AddArticleAsync(article);
            await _unitOfWork.CommitAsync();

            ArticleDetailDto articleDetailDto = _mapper.Map<ArticleDetailDto>(article);
            return articleDetailDto;
        }

        public async Task AddContributorToArticleAsync(int articleId, int contributorId)
        {
            Article article = await _unitOfWork.Articles.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            Contributor contributor = await _unitOfWork.Contributors.GetContributorByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);

            if (article.Contributors.Any(m => m.Id == contributorId))
            {
                throw new ConflictException(ArticleErrorCodes.ContributorAlreadyAddedToArticle);
            }

            article.Contributors.Add(contributor);
            await _unitOfWork.CommitAsync();
        }

        public async Task AddImageToArticleAsync(int articleId, int imageId, AddArticleImageDto addArticleImageDto)
        {
            Article article = await _unitOfWork.Articles.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            ImageMedia imageMedia = await _unitOfWork.Images.GetImageItemByIdAsync(imageId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);

            if (article.ArticleImages.Any(p => p.ImageId == imageMedia.Id))
            {
                throw new ConflictException(ArticleErrorCodes.ImageAlreadyAddedToArticle);
            }
            if (article.ArticleImages.Any(p => p.IsThumbnail && addArticleImageDto.IsThumbnail))
            {
                throw new ConflictException(ArticleErrorCodes.ArticleHasThumbnailImage);
            }

            ArticleImage newImage = new ArticleImage()
            {
                Image = imageMedia,
                ImageId = imageMedia.Id,
                Article = article,
                ArticleId = article.Id,
                IsThumbnail = addArticleImageDto.IsThumbnail
            };

            article.ArticleImages.Add(newImage);
            await _unitOfWork.CommitAsync();
        }

        public async Task AddTagToArticleAsync(int articleId, int tagId)
        {
            Article article = await _unitOfWork.Articles.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            Tag tag = await _tagRepository.GetTagByIdAsync(tagId) ?? throw new NotFoundException(TagErrorCodes.NotFound);

            if (article.Tags.Any(m => m.Id == tagId))
            {
                throw new ConflictException(ArticleErrorCodes.TagAlreadyAddedToArticle);
            }

            article.Tags.Add(tag);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteArticleAsync(int articleId)
        {
            Article article = await _unitOfWork.Articles.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);

            _unitOfWork.Articles.DeleteArticle(article);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteContributorFromArticleAsync(int articleId, int contributorId)
        {
            Article article = await _unitOfWork.Articles.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            Contributor contributor = await _unitOfWork.Contributors.GetContributorByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);
            if (!article.Contributors.Any(c => c.Id == contributorId))
            {
                throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);
            }
            article.Contributors.Remove(contributor);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteImageFromArticleAsync(int articleId, int imageId)
        {
            Article article = await _unitOfWork.Articles.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            ImageMedia imageMedia = await _unitOfWork.Images.GetImageItemByIdAsync(imageId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);

            ArticleImage? articleImage = article.ArticleImages.FirstOrDefault(i => i.ImageId == imageMedia.Id && i.ImageId == article.Id);
            if (articleImage == null)
            {
                throw new NotFoundException(ImageErrorCodes.ImageNotFound);
            }

            article.ArticleImages.Remove(articleImage);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteTagFromArticleAsync(int articleId, int tagId)
        {
            Article article = await _unitOfWork.Articles.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            Tag tag = await _tagRepository.GetTagByIdAsync(tagId) ?? throw new NotFoundException(TagErrorCodes.NotFound);
            if (!article.Tags.Any(c => c.Id == tagId))
            {
                throw new NotFoundException(TagErrorCodes.NotFound);
            }
            article.Tags.Remove(tag);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ArticleSummaryDto>> GetAllArticlesAsync(ArticleParameters articleParameters)
        {
            IReadOnlyList<Article> articles = await _unitOfWork.Articles.GetAllArticleAsync(articleParameters);
            List<ArticleSummaryDto> articleSummaryDtos = _mapper.Map<List<ArticleSummaryDto>>(articles);
            return articleSummaryDtos;
        }

        public async Task<ArticleDetailDto> GetArticleByIdAsync(int articleId)
        {
            Article article = await _unitOfWork.Articles.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            List<ArticleImageDto> imageDtos = new List<ArticleImageDto>();
            foreach (ArticleImage item in article.ArticleImages)
            {
                imageDtos.Add(_mapper.Map<ArticleImageDto>(item));
            }
            ArticleDetailDto articleDetailDto = _mapper.Map<ArticleDetailDto>(article);
            articleDetailDto.ArticleImages = imageDtos;
            return articleDetailDto;
        }

        public async Task LikeArticleAsync(int articleId, string userId)
        {
            Article article = await _unitOfWork.Articles.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            bool userExists = await _unitOfWork.Users.UserExistsAsync(userId);

            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            if (article.ArticleLikes.Any(u => u.UserId == userId))
            {
                throw new ConflictException(ArticleErrorCodes.AlreadyLikedByUser);
            }

            ArticleLike articleLike = new ArticleLike()
            {
                ArticleId = articleId,
                UserId = userId,
                Article = article,
            };

            article.ArticleLikes.Add(articleLike);
            await _unitOfWork.CommitAsync();
        }

        public async Task UnlikeArticleAsync(int articleId, string userId)
        {
            Article article = await _unitOfWork.Articles.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            bool userExists = await _unitOfWork.Users.UserExistsAsync(userId);
            ArticleLike? articleLike = article.ArticleLikes.FirstOrDefault(u => u.UserId == userId);

            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            if (articleLike == null)
            {
                throw new ConflictException(ArticleErrorCodes.DidNotLikeByUser);
            }

            article.ArticleLikes.Remove(articleLike);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ArticleDetailDto> UpdateArticleAsync(int articleId, UpdateArticleDto updateArticleDto)
        {
            Article article = await _unitOfWork.Articles.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            article = _mapper.Map(updateArticleDto, article);

            ArticleCategory articleCategory = await _unitOfWork.ArticleCategories.GetArticleCategoryByIdAsync(updateArticleDto.CategoryId) ?? throw new NotFoundException(ArticleCategoryErrorCodes.NotFound);
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

            _unitOfWork.Articles.UpdateArticle(article);
            await _unitOfWork.CommitAsync();

            ArticleDetailDto articleDetailDto = _mapper.Map<ArticleDetailDto>(article);
            return articleDetailDto;
        }

        public async Task ViewArticleAsync(int articleId)
        {
            Article article = await _unitOfWork.Articles.GetArticleByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            article.ViewCount++;

            _unitOfWork.Articles.UpdateArticle(article);
            await _unitOfWork.CommitAsync();
        }
    }
}