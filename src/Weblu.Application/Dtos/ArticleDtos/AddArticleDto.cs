using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.ArticleDtos
{
    public class AddArticleDto
    {
        public string Title { get; set; } = default!;
        public string? AboveTitle { get; set; }
        public string? BelowTitle { get; set; }
        public string Context { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ShortDescription { get; set; } = default!;
        public bool IsPublished { get; set; } = false;
        public int CategoryId { get; set; }

    }
}