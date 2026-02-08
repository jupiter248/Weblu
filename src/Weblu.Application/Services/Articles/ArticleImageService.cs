using Weblu.Application.Dtos.Articles.ArticleDtos.ArticleImageDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Articles;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Repositories.Images;
using Weblu.Application.Interfaces.Services.Articles;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Errors.Articles;
using Weblu.Domain.Errors.Images;

namespace Weblu.Application.Services.Articles
{
    public class ArticleImageService : IArticleImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArticleRepository _articleRepository;

        public ArticleImageService(IImageRepository imageRepository, IUnitOfWork unitOfWork, IArticleRepository articleRepository)
        {
            _imageRepository = imageRepository;
            _unitOfWork = unitOfWork;
            _articleRepository = articleRepository;
        }
        public async Task AddAsync(int articleId, int imageId, AddArticleImageDto addArticleImageDto)
        {
            Article article = await _articleRepository.GetByIdWithImagesAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            ImageMedia imageMedia = await _imageRepository.GetByIdAsync(imageId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);

            ArticleImage newImage = new ArticleImage()
            {
                Image = imageMedia,
                ImageId = imageMedia.Id,
                Article = article,
                ArticleId = article.Id,
                IsThumbnail = addArticleImageDto.IsThumbnail
            };

            article.AddImage(newImage);
            await _unitOfWork.CommitAsync();
        }
        public async Task DeleteAsync(int articleId, int imageId)
        {
            Article article = await _articleRepository.GetByIdWithImagesAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            ImageMedia imageMedia = await _imageRepository.GetByIdAsync(imageId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);

            article.DeleteImage(imageMedia);
            await _unitOfWork.CommitAsync();
        }
    }
}