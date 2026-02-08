namespace Weblu.Application.Interfaces.Services.Articles
{
    public interface IArticleContributorService
    {
        Task AddAsync(int articleId, int contributorId);
        Task DeleteAsync(int articleId, int contributorId);
    }
}