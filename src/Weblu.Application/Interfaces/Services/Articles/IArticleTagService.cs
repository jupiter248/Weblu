namespace Weblu.Application.Interfaces.Services.Articles
{
    public interface IArticleTagService
    {
        Task AddTagAsync(int articleId, int tagId);
        Task DeleteTagAsync(int articleId, int tagId);
    }
}