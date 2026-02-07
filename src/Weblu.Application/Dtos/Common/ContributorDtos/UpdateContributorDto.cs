namespace Weblu.Application.Dtos.Common.ContributorDtos
{
    public class UpdateContributorDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Role { get; set; }
        public string? Bio { get; set; }
        public string? Email { get; set; }
        public string? GithubUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public bool IsActive { get; set; }
    }
}