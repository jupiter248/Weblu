using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.MethodDtos
{
    public class AddMethodDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}