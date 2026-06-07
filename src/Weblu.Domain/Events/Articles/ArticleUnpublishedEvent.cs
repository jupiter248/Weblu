using Weblu.Domain.Events.Common;

namespace Weblu.Domain.Events.Articles
{
    public class ArticleUnpublishedEvent : IDomainEvent
    {
        public Guid ArticleId { get; }
        public DateTimeOffset OccurredOn { get; } = DateTimeOffset.Now;
        public ArticleUnpublishedEvent(Guid articleId)
        {
            ArticleId = articleId;
        }
    }
}