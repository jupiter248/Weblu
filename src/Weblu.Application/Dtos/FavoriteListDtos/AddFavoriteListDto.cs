using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Enums.Favorites;

namespace Weblu.Application.Dtos.FavoriteListDtos
{
    public class AddFavoriteListDto
    {
        public required string Name { get; set; }
        public FavoriteListType FavoriteListType { get; set; }
    }
}