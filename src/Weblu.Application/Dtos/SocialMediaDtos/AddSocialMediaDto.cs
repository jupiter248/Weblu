using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.SocialMediaDtos
{
    public class AddSocialMediaDto
    {
        public string Name { get; set; } = default!;
        public string? Link { get; set; }
    }
}