using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Enums.Favorites;

namespace Weblu.Domain.Entities.Favorites
{
    public class FavoriteList : BaseEntity
    {
        public required string Name { get; set; }
        public FavoriteListType FavoriteListType { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public List<FavoritePortfolio> FavoritePortfolios { get; set; } = new List<FavoritePortfolio>();
        public string UserId { get; set; } = default!;
    }
}