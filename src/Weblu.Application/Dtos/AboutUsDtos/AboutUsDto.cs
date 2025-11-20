using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.AboutUsDtos
{
    public class AboutUsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string SubTitle { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Vision { get; set; } = default!;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? HeadImageUrl { get; set; }
        public string? HeadImageAltText { get; set; }
        public string? UpdatedAt { get; set; }
        public string CreatedAt { get; set; } = default!;
    }
}