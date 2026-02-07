using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Media;

namespace Weblu.Domain.Entities.Articles
{
    public class ArticleImage : BaseEntity
    {
        public int ArticleId { get; set; }
        public Article Article { get; set; } = null!;
        public int ImageId { get; set; }
        public ImageMedia Image { get; set; } = null!;
        public bool IsThumbnail { get; set; }
        
    }
}