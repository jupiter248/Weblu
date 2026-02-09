using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Media;

namespace Weblu.Domain.Entities.Articles
{
    public class ArticleImage : BaseEntity
    {
        // Required properties
        public bool IsThumbnail { get; set; }
        // Relationships
        public int ArticleId { get; set; }
        public Article Article { get; set; } = default!;
        public int ImageId { get; set; }
        public ImageMedia Image { get; set; } = default!;

    }
}