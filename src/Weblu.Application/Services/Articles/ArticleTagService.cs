using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services.Articles;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Tags;
using Weblu.Domain.Errors.Articles;
using Weblu.Domain.Errors.Tags;

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
        public async Task AddTagAsync(int articleId, int tagId)
        {
            Article article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            Tag tag = await _tagRepository.GetByIdAsync(tagId) ??
            throw new NotFoundException(TagErrorCodes.NotFound);

            await _articleRepository.LoadTagsAsync(article);

            article.AddTag(tag);
            await _unitOfWork.CommitAsync();
        }
        public async Task DeleteTagAsync(int articleId, int tagId)
        {
            Article article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            Tag tag = await _tagRepository.GetByIdAsync(tagId) ?? throw new NotFoundException(TagErrorCodes.NotFound);

            await _articleRepository.LoadTagsAsync(article);

            article.DeleteTag(tag);
            await _unitOfWork.CommitAsync();
        }
    }
}