using Weblu.Domain.Enums.Common.Search;

namespace Weblu.Domain.Entities.Common.Search
{
    public class SearchItem : BaseEntity
    {
        public int EntityId { get; set; }
        public SearchEntityType EntityType { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTimeOffset IndexedAt { get; set; } = DateTimeOffset.Now;
    }
}