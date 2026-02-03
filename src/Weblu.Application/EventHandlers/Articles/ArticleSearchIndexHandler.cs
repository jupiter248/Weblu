using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Search;
using Weblu.Domain.Enums.Common.Search;
using Weblu.Domain.Errors.Articles;
using Weblu.Domain.Events.Articles;
using Weblu.Domain.Interfaces;

namespace Weblu.Application.EventHandlers.Articles
{
    public class ArticleSearchIndexHandler : IDomainEventHandler<ArticleAddedEvent>
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ArticleSearchIndexHandler(ISearchRepository searchRepository, IArticleRepository articleRepository, IUnitOfWork unitOfWork)
        {
            _searchRepository = searchRepository;
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(ArticleAddedEvent domainEvent)
        {
            Article? article = await _articleRepository.GetByPublicIdAsync(domainEvent.ArticleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            SearchItem searchItem = new SearchItem()
            {
                EntityId = article.Id,
                EntityType = SearchEntityType.Article,
                Content = article.Text,
                Title = article.Title
            };
            await _searchRepository.IndexAsync(searchItem);
            await _unitOfWork.CommitAsync();
        }
    }
}