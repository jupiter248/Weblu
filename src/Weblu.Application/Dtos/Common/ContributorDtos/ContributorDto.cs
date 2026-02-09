namespace Weblu.Application.DTOs.Common.ContributorDTOs
{
    public class ContributorDTO
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Role { get; set; }
        public string? Bio { get; set; }
        public string? Email { get; set; }
        public string? GithubUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? ProfileImageUrl { get; set; }
        public bool IsPublished { get; set; }
        public string? PublishedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public required string CreatedAt { get; set; }
        // public List<PortfolioSummaryDTO>? Portfolios { get; set; }
    }
}