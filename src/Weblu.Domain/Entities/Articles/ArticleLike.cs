using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Entities.Articles
{
    public class ArticleLike
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public DateTimeOffset LikedAt { get; private set; } = DateTimeOffset.Now;
    }
}