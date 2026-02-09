using Weblu.Application.Dtos.Portfolios.PortfolioDtos.PortfolioImageDtos;

namespace Weblu.Application.Dtos.Portfolios.PortfolioDtos
{
    public class PortfolioDetailDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int ReadingTimeMinutes { get; set; }
        public required string Description { get; set; }
        public required string ShortDescription { get; set; }
        public required string Slug { get; set; }
        public string? GithubUrl { get; set; }
        public string? LiveUrl { get; set; }
        public bool IsPublished { get; set; }
        public string? PublishedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public required string CreatedAt { get; set; }
        public int PortfolioCategoryId { get; set; }
        public required string PortfolioCategoryName { get; set; }
        public List<PortfolioImageDto>? Images { get; set; }

    }
}