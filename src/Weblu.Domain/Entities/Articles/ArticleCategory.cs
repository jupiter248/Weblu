using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.Articles
{
    public class ArticleCategory : BaseEntity
    {
        // Required properties
        public required string Name { get; set; }
        public string? Description { get; set; }
        // Relationships
        public List<Article> Articles { get; set; } = new();
    }
}