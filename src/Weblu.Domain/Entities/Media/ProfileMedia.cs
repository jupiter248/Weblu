using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Users;
using Weblu.Domain.Enums.Common.Media;

namespace Weblu.Domain.Entities.Media
{
    public class ProfileMedia : Media
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public long FileSize { get; set; }
        public required string OwnerId { get; set; }
        public ProfileMediaType OwnerType { get; set; } // User , Writer
        public bool IsMain { get; set; }
    }
}