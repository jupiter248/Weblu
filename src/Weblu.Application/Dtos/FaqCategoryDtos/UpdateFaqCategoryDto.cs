using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.FaqCategoryDtos
{
    public class UpdateFaqCategoryDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}