using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.FeatureDtos
{
    public class UpdateFeatureDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}