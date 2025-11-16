using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Enums.Favorites;

namespace Weblu.Application.Dtos.FavoriteListDtos
{
    public class FavoriteListDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public FavoriteListType FavoriteListType { get; set; }
        public int ItemCount { get; set; }
        public string? UpdatedAt { get; set; }
        public string CreatedAt { get; set; } = default!;
        public string UserId { get; set; } = default!;
    }
}