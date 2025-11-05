using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.PortfolioDtos;

namespace Weblu.Application.Dtos.ContributorDtos
{
    public class ContributorDto
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
        public bool IsActive { get; set; }
        public string? UpdatedAt { get; set; }
        public required string CreatedAt { get; set; }
        // public List<PortfolioSummaryDto>? Portfolios { get; set; }
    }
}