namespace Weblu.Application.Interfaces.Repositories.Common
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}