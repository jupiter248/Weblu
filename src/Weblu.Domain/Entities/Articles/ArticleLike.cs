using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.Articles
{
    public class ArticleLike : BaseEntity
    {
        public int ArticleId { get; set; }
        public Article Article { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public DateTimeOffset LikedAt { get; private set; } = DateTimeOffset.Now;
    }
}