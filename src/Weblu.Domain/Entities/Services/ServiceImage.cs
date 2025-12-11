using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Media;

namespace Weblu.Domain.Entities.Services
{
    public class ServiceImage : BaseEntity
    {
        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;
        public int ImageId { get; set; }
        public ImageMedia Image { get; set; } = null!;
        public bool IsThumbnail { get; set; }

    }
}