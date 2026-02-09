using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.Articles
{
    public class ArticleLike : BaseEntity
    {
        // Relationships
        public int ArticleId { get; set; }
        public Article Article { get; set; } = default!;
        public string UserId { get; set; } = default!;
    }
}