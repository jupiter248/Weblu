namespace Weblu.Application.Interfaces.Services.Portfolios
{
    public interface IPortfolioFeatureService
    {
        Task AddAsync(int portfolioId, int featureId);
        Task DeleteAsync(int portfolioId, int featureId);
    }
}