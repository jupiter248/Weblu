namespace Weblu.Application.Interfaces.Services.Portfolios
{
    public interface IPortfolioMethodService
    {
        Task AddAsync(int portfolioId, int methodId);
        Task DeleteAsync(int portfolioId, int methodId);
    }
}