using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.About
{
    public class SocialMedia : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Link { get; set; }
        public string? IconUrl { get; set; }
        public string? IconAltText { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
    }
}