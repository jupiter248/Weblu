using Weblu.Domain.Enums.Users.Favorites;

namespace Weblu.Application.DTOs.Users.Favorites.FavoriteListDTOs
{
    public class CreateFavoriteListDTO
    {
        public required string Name { get; set; }
        public FavoriteListType FavoriteListType { get; set; }
    }
}