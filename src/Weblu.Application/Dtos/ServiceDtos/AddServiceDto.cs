using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.ServiceDtos
{
    public class AddServiceDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string ShortDescription { get; set; }
        public int DurationInDays { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; }
    }
}