using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Users;

namespace Weblu.Domain.Entities.Media
{
    public class ProfileMedia : Media
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public long FileSize { get; set; }
        public List<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
    }
}