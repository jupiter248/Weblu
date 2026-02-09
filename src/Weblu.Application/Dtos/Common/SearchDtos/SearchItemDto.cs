using Weblu.Domain.Enums.Common.Search;

namespace Weblu.Application.DTOs.Common.SearchDTOs
{
    public class SearchItemDTO
    {
        public int EntityId { get; set; }
        public SearchEntityType EntityType { get; set; }
        public string Title { get; set; } = default!;
    }
}