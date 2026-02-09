namespace Weblu.Application.Dtos.Portfolios.PortfolioDtos
{
    public class PortfolioSummaryDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public required string ShortDescription { get; set; }
        public bool IsPublished { get; set; }
        public string? ThumbnailPictureUrl { get; set; }
    }
}