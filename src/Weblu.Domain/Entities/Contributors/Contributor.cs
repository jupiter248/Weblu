using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Portfolios;

namespace Weblu.Domain.Entities.Contributors
{
    public class Contributor : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Role { get; set; }
        public string? Bio { get; set; }
        public string? Email { get; set; }
        public string? GithubUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? ProfileImageAltText { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
        public List<Article> Articles { get; set; } = new List<Article>();

    }
}