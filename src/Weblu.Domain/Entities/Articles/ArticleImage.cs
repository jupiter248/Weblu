using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Media;

namespace Weblu.Domain.Entities.Articles
{
    public class ArticleImage
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; } = null!;
        public int ImageId { get; set; }
        public ImageMedia Image { get; set; } = null!;
        public bool IsThumbnail { get; set; }
        
    }
}