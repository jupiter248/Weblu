namespace Weblu.Application.Interfaces.Repositories.Users
{
    public interface IRoleRepository
    {
        public Task<List<string>> GetRolePermissionsAsync(IEnumerable<string> roleNames);

    }
}