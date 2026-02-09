namespace Weblu.Application.Interfaces.Services.Articles
{
    public interface IArticleTagService
    {
        Task AddAsync(int articleId, int tagId);
        Task DeleteAsync(int articleId, int tagId);
    }
}