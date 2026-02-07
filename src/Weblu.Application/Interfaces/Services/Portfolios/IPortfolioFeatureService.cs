namespace Weblu.Application.Interfaces.Services.Portfolios
{
    public interface IPortfolioFeatureService
    {
        Task AddFeatureAsync(int portfolioId, int featureId);
        Task DeleteFeatureAsync(int portfolioId, int featureId);
    }
}