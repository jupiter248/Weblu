using System.Reflection;

namespace Weblu.Infrastructure.Identity.Authorization
{
    public class Permissions
    {
        public const string ManageAdmins = "Permissions.Admins.Manage";
        public const string ManageUsers = "Permissions.Users.Manage";
        public const string ManageComments = "Permissions.Comments.Manage";
        public const string ManageArticles = "Permissions.Articles.Manage";
        public const string ManagePortfolios = "Permissions.Portfolios.Manage";
        public const string ManageServices = "Permissions.Services.Manage";
        public const string ManageAboutUs = "Permissions.AboutUs.Manage";
        public const string ManageContributors = "Permissions.Contributors.Manage";
        public const string ManageFAQs = "Permissions.FAQs.Manage";
        public const string ManageFeatures = "Permissions.Features.Manage";
        public const string ManageMethods = "Permissions.Methods.Manage";
        public const string ManageImages = "Permissions.Images.Manage";
        public const string ManageProfiles = "Permissions.Profiles.Manage";
        public const string ManageSocialMedia = "Permissions.SocialMedia.Manage";
        public const string ManageTags = "Permissions.Tags.Manage";
        public const string ManageTickets = "Permissions.Tickets.Manage";










        public static IEnumerable<string> All =>
            typeof(Permissions)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.FieldType == typeof(string))
                .Select(f => (string)f.GetValue(null)!);
    }
}