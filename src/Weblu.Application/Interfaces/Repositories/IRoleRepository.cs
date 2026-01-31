namespace Weblu.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        public Task<List<string>> GetRolePermissionsAsync(IEnumerable<string> roleNames);

    }
}