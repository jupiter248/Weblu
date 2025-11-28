using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.ArticleCategoryDtos
{
    public class AddArticleCategoryDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}