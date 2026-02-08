namespace Weblu.Application.Interfaces.Services.Users.Favorites.FavoriteLists
{
    public interface IFavoriteListArticleService
    {
        Task AddAsync(string userId, int favoriteListId, int articleId);
        Task DeleteAsync(string userId, int favoriteListId, int articleId);
    }
}