using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.FaqDtos
{
    public class AddFaqDto
    {
        public string Question { get; set; } = default!;
        public string Answer { get; set; } = default!;
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
    }
}