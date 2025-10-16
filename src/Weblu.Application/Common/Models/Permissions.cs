using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Common.Models
{
    public class Permissions
    {
        public const string AddAdmin = "Permissions.Admins.Add";
        public const string ManageUsers = "Permissions.Users.Manage";
        public const string ViewDashboard = "Permissions.Dashboard.View";
    }
}