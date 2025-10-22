using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Media;

namespace Weblu.Domain.Entities.Users
{
    public class UserProfile
    {
        public int Id { get; set; }
        public int ProfileMediaId { get; set; }
        public ProfileMedia ProfileMedia { get; set; } = null!;
        public required string UserId { get; set; }
        public bool IsMain { get; set; }
    }
}