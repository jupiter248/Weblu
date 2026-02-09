using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Entities.Services;

namespace Weblu.Domain.Entities.Common.Methods
{
    public class Method : BaseEntity
    {
        // Required properties
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageAltText { get; set; }
        // Relationships
        public List<Service> Services { get; set; } = new();
        public List<Portfolio> Portfolios { get; set; } = new();

    }
}