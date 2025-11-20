using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.AboutUsDtos
{
    public class UpdateAboutUsDto
    {
        public string Title { get; set; } = default!;
        public string SubTitle { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Vision { get; set; } = default!;
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}