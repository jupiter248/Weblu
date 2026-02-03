using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Enums.Common.Search;

namespace Weblu.Application.Dtos.SearchDtos
{
    public class SearchItemDto
    {
        public int EntityId { get; set; }
        public SearchEntityType EntityType { get; set; }
        public string Title { get; set; } = default!;
    }
}