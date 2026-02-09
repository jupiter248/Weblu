using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Errors.Common;
using Weblu.Domain.Exceptions;

namespace Weblu.Domain.Entities.Common.Contributors
{
    public class Contributor : BaseEntity
    {
        // Required properties
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Role { get; set; } = default!;
        public string? Bio { get; set; }
        public string? Email { get; set; }
        public string? GithubUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? ProfileImageAltText { get; set; }
        // Publishing info
        public bool IsPublished { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }
        // Relationships
        public List<Portfolio> Portfolios { get; set; } = new();
        public List<Article> Articles { get; set; } = new();

        public void Publish()
        {
            if (IsPublished) throw new DomainException(ContributorErrorCodes.AlreadyPublished, 409);
            IsPublished = true;
            PublishedAt = DateTimeOffset.Now;
        }
        public void Unpublish()
        {
            if (!IsPublished) throw new DomainException(ContributorErrorCodes.DidNotPublish, 409);
            IsPublished = false;
            PublishedAt = null;
        }
    }
}