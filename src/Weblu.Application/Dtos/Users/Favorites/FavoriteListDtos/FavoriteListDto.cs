using Weblu.Domain.Enums.Users.Favorites;

namespace Weblu.Application.Dtos.Users.Favorites.FavoriteListDtos
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