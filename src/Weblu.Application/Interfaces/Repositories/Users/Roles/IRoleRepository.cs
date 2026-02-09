namespace Weblu.Application.Interfaces.Repositories.Users.Roles
{
    public interface IRoleRepository
    {
        public Task<List<string>> GetRolePermissionsAsync(IEnumerable<string> roleNames);

    }
}