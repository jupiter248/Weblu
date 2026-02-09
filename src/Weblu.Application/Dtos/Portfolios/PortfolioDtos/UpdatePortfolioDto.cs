namespace Weblu.Application.DTOs.Portfolios.PortfolioDTOs
{
    public class UpdatePortfolioDTO
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string ShortDescription { get; set; }
        public string? GithubUrl { get; set; }
        public string? LiveUrl { get; set; }
        public int PortfolioCategoryId { get; set; }
    }
}