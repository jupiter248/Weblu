using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.SocialMediaDtos
{
    public class SocialMediaDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Link { get; set; }
        public string? IconUrl { get; set; }
        public string? IconAltText { get; set; }
        public string? UpdatedAt { get; set; }
        public string CreatedAt { get; set; } = default!;
    }
}