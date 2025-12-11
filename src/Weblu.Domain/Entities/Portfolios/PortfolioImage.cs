using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Media;

namespace Weblu.Domain.Entities.Portfolios
{
    public class PortfolioImage : BaseEntity
    {
        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; } = null!;
        public int ImageMediaId { get; set; }
        public ImageMedia ImageMedia { get; set; } = null!;
        public bool IsThumbnail { get; set; }
    }
}