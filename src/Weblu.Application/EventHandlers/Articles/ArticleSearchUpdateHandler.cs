using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories.Articles;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Common.Search;
using Weblu.Domain.Enums.Common.Search;
using Weblu.Domain.Errors.Articles;
using Weblu.Domain.Errors.Common;
using Weblu.Domain.Events.Articles;
using Weblu.Domain.Interfaces;

namespace Weblu.Application.EventHandlers.Articles
{
    public class ArticleSearchUpdateHandler : IDomainEventHandler<ArticleUpdatedEvent>
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ArticleSearchUpdateHandler(ISearchRepository searchRepository, IArticleRepository articleRepository, IUnitOfWork unitOfWork)
        {
            _searchRepository = searchRepository;
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(ArticleUpdatedEvent domainEvent)
        {
            Article? article = await _articleRepository.GetByGuidIdAsync(domainEvent.ArticleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            SearchItem? searchItem = await _searchRepository.GetByEntityIdAsync(article.Id, SearchEntityType.Article) ?? throw new NotFoundException(SearchErrorCodes.NotFound);
            searchItem.Title = article.Title;
            searchItem.Content = article.Text;
            _searchRepository.Update(searchItem);
            await _unitOfWork.CommitAsync();
        }
    }
}