using Weblu.Domain.Enums.Common.Search;

namespace Weblu.Application.Dtos.Common.SearchDtos
{
    public class SearchItemDto
    {
        public int EntityId { get; set; }
        public SearchEntityType EntityType { get; set; }
        public string Title { get; set; } = default!;
    }
}