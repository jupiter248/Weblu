namespace Weblu.Application.DTOs.Common.ContributorDTOs
{
    public class CreateContributorDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Role { get; set; }
        public string? Bio { get; set; }
        public string? Email { get; set; }
        public string? GithubUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public bool IsPublished { get; set; }
    }
}