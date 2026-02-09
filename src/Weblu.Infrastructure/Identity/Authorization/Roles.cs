using System.Reflection;

namespace Weblu.Infrastructure.Identity.Authorization
{
    public class Roles
    {
        public const string HeadAdmin = "Head-Admin";
        public const string Admin = "Admin";
        public const string Editor = "Editor";
        public const string User = "User";
        public static IEnumerable<string> All =>
            typeof(Roles)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(string))
            .Select(f => (string)f.GetValue(null)!);
    }
}