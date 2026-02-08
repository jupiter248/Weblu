namespace Weblu.Application.Interfaces.Services.Portfolios
{
    public interface IPortfolioContributorService
    {
        Task AddAsync(int portfolioId, int contributorId);
        Task DeleteAsync(int portfolioId, int contributorId);
    }
}