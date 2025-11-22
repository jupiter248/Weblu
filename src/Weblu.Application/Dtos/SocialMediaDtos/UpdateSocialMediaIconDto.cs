using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Weblu.Application.Dtos.SocialMediaDtos
{
    public class UpdateSocialMediaIconDto
    {
        public IFormFile Icon { get; set; } = default!;
        public string? AltText { get; set; }

    }
}