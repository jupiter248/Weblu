using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Entities.Portfolios
{
    public class PortfolioImage
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; } = null!;
        public int ImageId { get; set; }
        public PortfolioImage Image { get; set; } = null!;
        public bool IsThumbnail { get; set; }
    }
}