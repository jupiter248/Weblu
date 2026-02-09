using Weblu.Domain.Enums.Users.Favorites;

namespace Weblu.Application.Dtos.Users.Favorites.FavoriteListDtos
{
    public class CreateFavoriteListDto
    {
        public required string Name { get; set; }
        public FavoriteListType FavoriteListType { get; set; }
    }
}