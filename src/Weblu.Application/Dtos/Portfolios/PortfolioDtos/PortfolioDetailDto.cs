using Weblu.Application.DTOs.Portfolios.PortfolioDTOs.PortfolioImageDTOs;

namespace Weblu.Application.DTOs.Portfolios.PortfolioDTOs
{
    public class PortfolioDetailDTO
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
        public List<PortfolioImageDTO>? Images { get; set; }

    }
}