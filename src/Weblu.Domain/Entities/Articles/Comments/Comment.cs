using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.Articles.Comments
{
    public class Comment : BaseEntity
    {
        // Required properties
        public string Text { get; set; } = default!;
        public int? ParentCommentId { get; set; }
        public string UserId { get; set; } = default!;
        // Relationships
        public int ArticleId { get; set; } = default!;
        public Article Article { get; set; } = default!;
    }
}