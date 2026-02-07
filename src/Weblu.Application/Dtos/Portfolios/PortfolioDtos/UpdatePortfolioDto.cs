namespace Weblu.Application.Dtos.Portfolios.PortfolioDtos
{
    public class UpdatePortfolioDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string ShortDescription { get; set; }
        public string? GithubUrl { get; set; }
        public string? LiveUrl { get; set; }
        public bool IsActive { get; set; }
        public int PortfolioCategoryId { get; set; }
    }
}