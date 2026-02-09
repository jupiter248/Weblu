using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Articles;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Services.Articles;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Common.Tags;
using Weblu.Domain.Errors.Articles;
using Weblu.Domain.Errors.Common;

namespace Weblu.Application.Services.Articles
{
    public class ArticleTagService : IArticleTagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArticleRepository _articleRepository;

        public ArticleTagService(ITagRepository tagRepository, IUnitOfWork unitOfWork, IArticleRepository articleRepository)
        {
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
            _articleRepository = articleRepository;
        }
        public async Task AddAsync(int articleId, int tagId)
        {
            Article article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            Tag tag = await _tagRepository.GetByIdAsync(tagId) ??
            throw new NotFoundException(TagErrorCodes.NotFound);

            await _articleRepository.LoadTagsAsync(article);

            article.AddTag(tag);
            await _unitOfWork.CommitAsync();
        }
        public async Task DeleteAsync(int articleId, int tagId)
        {
            Article article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            Tag tag = await _tagRepository.GetByIdAsync(tagId) ?? throw new NotFoundException(TagErrorCodes.NotFound);

            await _articleRepository.LoadTagsAsync(article);

            article.RemoveTag(tag);
            await _unitOfWork.CommitAsync();
        }
    }
}