using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Entities.Services;

namespace Weblu.Domain.Entities.Media
{
    public class ImageMedia : Media
    {
        // Required properties
        public int Width { get; set; }
        public int Height { get; set; }
        // Relationships
        public List<ServiceImage> ServiceImages { get; set; } = new();
        public List<PortfolioImage> PortfolioImages { get; set; } = new();
        public List<ArticleImage> ArticleImages { get; set; } = new();

    }
}