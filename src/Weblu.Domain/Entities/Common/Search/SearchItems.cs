using Weblu.Domain.Enums.Common.Search;

namespace Weblu.Domain.Entities.Common.Search
{
    public class SearchItem : BaseEntity
    {
        // Required properties
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTimeOffset IndexedAt { get; set; } = DateTimeOffset.Now;
        // Relationships
        public int EntityId { get; set; }
        public SearchEntityType EntityType { get; set; }
    }
}