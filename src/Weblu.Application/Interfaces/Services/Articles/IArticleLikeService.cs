namespace Weblu.Application.Interfaces.Services.Articles
{
    public interface IArticleLikeService
    {
        Task LikeAsync(int articleId, string userId);
        Task UnlikeAsync(int articleId, string userId);
    }
}