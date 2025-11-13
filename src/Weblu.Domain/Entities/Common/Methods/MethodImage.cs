using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Media;

namespace Weblu.Domain.Entities.Common.Methods
{
    public class MethodImage
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public ImageMedia Image { get; set; } = default!;
        public int MethodId { get; set; }
        public Method Method { get; set; } = default!;
    }
}