using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Events.Common;

namespace Weblu.Domain.Events.Articles
{
    public sealed class ArticleAddedEvent : IDomainEvent
    {
        public Guid ArticleId { get; }
        public DateTimeOffset OccurredOn { get; } = DateTimeOffset.Now;
        public ArticleAddedEvent(Guid articleId)
        {
            ArticleId = articleId;
        }
    }
}